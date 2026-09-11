---
name: java-development
description: Java development best practices and patterns
version: 1.0.0
author: opencode
tags:
  - java
  - spring-boot
  - jpa
  - testing
capabilities:
  - spring-boot-development
  - jpa-hibernate
  - dependency-injection
  - rest-api-design
  - testing-strategies
  - performance-tuning
references:
  - "https://spring.io/projects/spring-boot"
  - "https://docs.spring.io/spring-framework/reference/"
  - "https://hibernate.org/orm/documentation/"
examples:
  - name: "Spring Boot Application"
    description: "Main application class with configuration"
    code: |
      @SpringBootApplication
      @EnableJpaRepositories(basePackages = "com.college.admission.repository")
      @EntityScan(basePackages = "com.college.admission.domain")
      class CollegeAdmissionApplication
      
      fun main(args: Array<String>) {
          runApplication<CollegeAdmissionApplication>(*args)
      }
  - name: "Repository Pattern"
    description: "JPA repository with custom queries"
    code: |
      interface StudentRepository : JpaRepository<Student, Long> {
          @Query("SELECT s FROM Student s WHERE s.email = :email")
          fun findByEmail(@Param("email") email: String): Optional<Student>
          
          @Query("SELECT s FROM Student s WHERE s.status = :status AND s.createdAt >= :date")
          fun findByStatusAndDateRange(
              @Param("status") status: ApplicationStatus,
              @Param("date") date: LocalDateTime,
              pageable: Pageable
          ): Page<Student>
          
          @Modifying
          @Query("UPDATE Student s SET s.status = :status WHERE s.id IN :ids")
          fun updateStatus(@Param("ids") ids: List<Long>, @Param("status") status: ApplicationStatus): Int
      }
  - name: "Service Layer"
    description: "Transactional service with proper error handling"
    code: |
      @Service
      @Transactional(readOnly = true)
      class StudentService(
          private val repository: StudentRepository,
          private val eventPublisher: ApplicationEventPublisher
      ) {
          @Transactional
          fun submitApplication(command: SubmitApplicationCommand): Result<Application> {
              return try {
                  val student = Student(command.toDomain())
                  val saved = repository.save(student)
                  eventPublisher.publishEvent(ApplicationSubmittedEvent(saved.id))
                  Result.success(saved)
              } catch (e: DataIntegrityViolationException) {
                  Result.failure(ApplicationError.DUPLICATE_EMAIL)
              }
          }
      }