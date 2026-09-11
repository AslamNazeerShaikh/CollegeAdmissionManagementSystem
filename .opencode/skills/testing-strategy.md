---
name: testing-strategy
description: Comprehensive testing strategies for Kotlin/Java applications
version: 1.0.0
author: opencode
tags:
  - testing
  - junit
  - mockk
  - testcontainers
  - integration-testing
  - contract-testing
capabilities:
  - unit-testing
  - integration-testing
  - contract-testing
  - testcontainers
  - test-fixtures
  - coverage-analysis
  - mutation-testing
references:
  - "https://junit.org/junit5/docs/current/user-guide/"
  - "https://mockk.io/"
  - "https://testcontainers.com/"
  - "https://pitest.org/"
examples:
  - name: "Test Configuration"
    description: "Gradle test setup with JUnit 5, MockK, TestContainers"
    code: |
      // build.gradle.kts
      dependencies {
          testImplementation(libs.junit.jupiter)
          testImplementation(libs.junit.jupiter.params)
          testImplementation(libs.mockito.junit)
          testImplementation(libs.mockk)
          testImplementation(libs.assertj)
          testImplementation(libs.testcontainers.junit)
          testImplementation(libs.testcontainers.postgresql)
          testImplementation(libs.testcontainers.kafka)
          
          testRuntimeOnly(libs.junit.jupiter.engine)
      }
      
      tasks.withType<Test> {
          useJUnitPlatform()
          testLogging {
              events("passed", "skipped", "failed", "standardOut", "standardError")
          }
          systemProperty("spring.profiles.active", "test")
      }
      
      // Jacoco coverage
      tasks.withType<JacocoReport> {
          reports {
              html.required.set(true)
              xml.required.set(true)
              csv.required.set(false)
          }
      }
  - name: "Unit Test Base"
    description: "Common test utilities and base classes"
    code: |
      // test/kotlin/com/college/admission/test/BaseUnitTest.kt
      abstract class BaseUnitTest {
          
          @BeforeEach
          fun setup() {
              MockKAnnotations.init(this)
          }
          
          @AfterEach
          fun teardown() {
              clearAllMocks()
          }
      }
      
      // test/kotlin/com/college/admission/test/TestFixtures.kt
      object TestFixtures {
          
          fun student(
              id: Long = 1,
              firstName: String = "John",
              lastName: String = "Doe",
              email: String = "john.doe@example.com",
              status: StudentStatus = StudentStatus.ACTIVE
          ): Student = Student(
              id = id,
              firstName = firstName,
              lastName = lastName,
              email = email,
              phone = "+1234567890",
              dateOfBirth = LocalDate.of(2000, 1, 15),
              address = "123 Test St",
              status = status,
              createdAt = Instant.now(),
              updatedAt = Instant.now()
          )
          
          fun application(
              id: Long = 1,
              student: Student = student(),
              program: Program = program(),
              status: ApplicationStatus = ApplicationStatus.SUBMITTED
          ): Application = Application(
              id = id,
              student = student,
              program = program,
              status = status,
              submittedAt = Instant.now(),
              documents = emptyList(),
              createdAt = Instant.now(),
              updatedAt = Instant.now()
          )
          
          fun program(
              id: Long = 1,
              name: String = "Computer Science",
              code: String = "CS",
              degree: Degree = Degree.BACHELOR
          ): Program = Program(
              id = id,
              name = name,
              code = code,
              degree = degree,
              description = "Test program",
              requirements = listOf("High school diploma", "Math proficiency"),
              capacity = 100,
              createdAt = Instant.now(),
              updatedAt = Instant.now()
          )
      }
  - name: "Integration Test with TestContainers"
    description: "Repository integration tests with real database"
    code: |
      @Testcontainers
      @SpringBootTest
      @AutoConfigureTestDatabase(replace = AutoConfigureTestDatabase.Replace.NONE)
      class StudentRepositoryIntegrationTest {
          
          @Container
          @JvmStatic
          val postgres: PostgreSQLContainer<*> = PostgreSQLContainer("postgres:15-alpine")
              .withDatabaseName("test_db")
              .withUsername("test")
              .withPassword("test")
          
          @DynamicPropertySource
          @JvmStatic
          fun configure(registry: DynamicPropertyRegistry) {
              registry.add("spring.datasource.url") { postgres.jdbcUrl }
              registry.add("spring.datasource.username") { postgres.username }
              registry.add("spring.datasource.password") { postgres.password }
              registry.add("spring.jpa.hibernate.ddl-auto") { "validate" }
              registry.add("spring.flyway.enabled") { "true" }
          }
          
          @Autowired
          lateinit var repository: StudentRepository
          
          @Autowired
          lateinit var entityManager: EntityManager
          
          @Test
          @Transactional
          fun `should save and find student by email`() {
              // Given
              val student = TestFixtures.student(email = "unique@test.com")
              
              // When
              val saved = repository.save(student)
              entityManager.flush()
              entityManager.clear()
              
              // Then
              val found = repository.findByEmail("unique@test.com")
              assertThat(found).isPresent()
              assertThat(found.get().email).isEqualTo("unique@test.com")
              assertThat(found.get().firstName).isEqualTo("John")
          }
          
          @Test
          @Transactional
          fun `should find applications by status with pagination`() {
              // Given
              val student = repository.save(TestFixtures.student())
              val program = entityManager.persistAndFlush(TestFixtures.program())
              
              repeat(25) { i ->
                  val app = TestFixtures.application(student = student, program = program)
                  if (i < 10) app.status = ApplicationStatus.SUBMITTED
                  else if (i < 20) app.status = ApplicationStatus.UNDER_REVIEW
                  else app.status = ApplicationStatus.APPROVED
                  entityManager.persist(app)
              }
              entityManager.flush()
              entityManager.clear()
              
              // When
              val page = repository.findByStatus(ApplicationStatus.SUBMITTED, PageRequest.of(0, 10))
              
              // Then
              assertThat(page.content).hasSize(10)
              assertThat(page.totalElements).isEqualTo(10)
              assertThat(page.content.allMatch { it.status == ApplicationStatus.SUBMITTED }).isTrue()
          }
      }
  - name: "Contract Testing with Pact"
    description: "Consumer-driven contract testing"
    code: |
      // Consumer test
      @ExtendWith(PactConsumerTestExt::class)
      class ApplicationApiConsumerTest {
          
          @Pact(consumer = "admission-portal", provider = "admission-api")
          fun createApplicationPact(builder: PactDslWithProvider): RequestResponsePact {
              return builder
                  .given("A valid program exists")
                  .uponReceiving("A request to submit an application")
                  .path("/api/v1/applications")
                  .method("POST")
                  .headers("Content-Type", "application/json")
                  .body(json {
                      "studentId" to 1
                      "programId" to 1
                      "documents" to emptyList()
                  })
                  .willRespondWith()
                  .status(201)
                  .headers("Content-Type", "application/json")
                  .body(json {
                      "id" to 1
                      "studentId" to 1
                      "programId" to 1
                      "status" to "SUBMITTED"
                      "submittedAt" to matcher("2024-01-15T10:30:00Z")
                  })
                  .toPact()
          }
          
          @Test
          @PactTestFor(pactMethod = "createApplicationPact")
          fun `should submit application`(mockServer: MockServer) {
              val client = WebClient.create(mockServer.url)
              val request = SubmitApplicationRequest(1, 1, emptyList())
              
              val response = client.post()
                  .uri("/api/v1/applications")
                  .bodyValue(request)
                  .retrieve()
                  .bodyToMono(ApplicationResponse::class.java)
                  .block()
              
              assertThat(response).isNotNull()
              assertThat(response!!.status).isEqualTo(ApplicationStatus.SUBMITTED)
          }
      }
      
      // Provider test
      @SpringBootTest
      @AutoConfigureMockMvc
      @PactVerification(value = "admission-api", fragment = "createApplicationPact")
      class ApplicationApiProviderTest {
          @Autowired
          lateinit var mockMvc: MockMvc
          
          @TestTemplate
          @ExtendWith(PactVerificationInvocationContextProvider::class)
          fun testTemplate(context: PactVerificationContext) {
              context.verifyInteraction()
          }
          
          @BeforeEach
          fun before(context: PactVerificationContext) {
              context.target = MockMvcTarget(mockMvc)
          }
      }