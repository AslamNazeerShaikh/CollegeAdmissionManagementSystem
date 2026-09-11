---
name: database-migration
description: Database migration strategies using Flyway/Liquibase
version: 1.0.0
author: opencode
tags:
  - flyway
  - liquibase
  - database
  - migration
  - schema
capabilities:
  - flyway-setup
  - liquibase-setup
  - migration-scripts
  - versioning-strategy
  - rollback-procedures
  - test-data-management
references:
  - "https://flywaydb.org/documentation/"
  - "https://docs.liquibase.com/"
examples:
  - name: "Flyway Configuration"
    description: "Gradle Flyway plugin setup"
    code: |
      // build.gradle.kts
      plugins {
          id("org.flywaydb.flyway") version "10.10.0"
      }
      
      flyway {
          url = providers.gradleProperty("db.url")
          user = providers.gradleProperty("db.user")
          password = providers.gradleProperty("db.password")
          locations = listOf("db/migration")
          baselineOnMigrate = true
          validateOnMigrate = true
          outOfOrder = false
      }
      
      // gradle.properties
      db.url=jdbc:postgresql://localhost:5432/college_admission
      db.user=postgres
      db.password=secret
  - name: "Migration Scripts"
    description: "Versioned SQL migration files"
    code: |
      -- db/migration/V1__initial_schema.sql
      CREATE TABLE students (
          id BIGSERIAL PRIMARY KEY,
          first_name VARCHAR(100) NOT NULL,
          last_name VARCHAR(100) NOT NULL,
          email VARCHAR(255) UNIQUE NOT NULL,
          phone VARCHAR(20),
          date_of_birth DATE,
          address TEXT,
          created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
          updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
      );
      
      CREATE TABLE applications (
          id BIGSERIAL PRIMARY KEY,
          student_id BIGINT NOT NULL REFERENCES students(id),
          program_id BIGINT NOT NULL,
          status VARCHAR(50) NOT NULL DEFAULT 'SUBMITTED',
          submitted_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
          reviewed_at TIMESTAMP WITH TIME ZONE,
          reviewer_id BIGINT,
          notes TEXT,
          created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
          updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
      );
      
      CREATE INDEX idx_applications_student ON applications(student_id);
      CREATE INDEX idx_applications_status ON applications(status);
      CREATE INDEX idx_applications_program ON applications(program_id);
      
      -- db/migration/V2__add_documents_table.sql
      CREATE TABLE documents (
          id BIGSERIAL PRIMARY KEY,
          application_id BIGINT NOT NULL REFERENCES applications(id),
          document_type VARCHAR(50) NOT NULL,
          file_name VARCHAR(255) NOT NULL,
          file_path VARCHAR(500) NOT NULL,
          mime_type VARCHAR(100),
          file_size BIGINT,
          uploaded_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
          verified BOOLEAN DEFAULT FALSE,
          verified_at TIMESTAMP WITH TIME ZONE,
          verified_by BIGINT
      );
      
      CREATE INDEX idx_documents_application ON documents(application_id);
  - name: "Test Migrations"
    description: "Run migrations in tests with TestContainers"
    code: |
      @Testcontainers
      @SpringBootTest
      class FlywayMigrationTest {
          
          @Container
          @JvmStatic
          val postgres: PostgreSQLContainer<*> = PostgreSQLContainer("postgres:15")
              .withDatabaseName("test")
              .withUsername("test")
              .withPassword("test")
          
          @DynamicPropertySource
          @JvmStatic
          fun configureProperties(registry: DynamicPropertyRegistry) {
              registry.add("spring.datasource.url") { postgres.jdbcUrl }
              registry.add("spring.datasource.username") { postgres.username }
              registry.add("spring.datasource.password") { postgres.password }
          }
          
          @Autowired
          lateinit var flyway: Flyway
          
          @Test
          fun `should apply all migrations successfully`() {
              val result = flyway.migrate()
              assertThat(result.migrationsExecuted).isGreaterThan(0)
          }
          
          @Test
          fun `should validate migrations`() {
              flyway.validate()
          }
      }