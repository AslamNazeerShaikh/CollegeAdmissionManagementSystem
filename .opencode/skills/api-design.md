---
name: api-design
description: REST API design principles and OpenAPI documentation
version: 1.0.0
author: opencode
tags:
  - rest
  - openapi
  - swagger
  - api-design
  - ktor
  - spring-web
capabilities:
  - rest-best-practices
  - openapi-specification
  - versioning
  - error-handling
  - pagination
  - filtering
  - authentication
references:
  - "https://restfulapi.net/"
  - "https://swagger.io/specification/"
  - "https://github.com/OpenAPITools/openapi-generator"
examples:
  - name: "OpenAPI Configuration"
    description: "SpringDoc OpenAPI setup"
    code: |
      @Configuration
      class OpenApiConfig {
          @Bean
          fun apiInfo(): OpenAPI {
              return OpenAPI()
                  .info(Info()
                      .title("College Admission API")
                      .version("1.0.0")
                      .description("API for managing college admissions")
                      .contact(Contact()
                          .name("College Admission Team")
                          .email("api@college.edu")
                      )
                      .license(License()
                          .name("MIT")
                          .url("https://opensource.org/licenses/MIT")
                      )
                  )
                  .servers(listOf(
                      Server().url("http://localhost:8080").description("Development"),
                      Server().url("https://api.college.edu").description("Production")
                  ))
                  .components(Components()
                      .addSecuritySchemes("bearerAuth", SecurityScheme()
                          .type(SecurityScheme.Type.HTTP)
                          .scheme("bearer")
                          .bearerFormat("JWT")
                      )
                  )
                  .addSecurityItem(SecurityRequirement().addList("bearerAuth"))
      }
  - name: "REST Controller"
    description: "Clean REST endpoints with proper HTTP semantics"
    code: |
      @RestController
      @RequestMapping("/api/v1/applications")
      @Tag(name = "Applications", description = "Application management endpoints")
      class ApplicationController(
          private val submitApplicationUseCase: SubmitApplicationUseCase,
          private val getApplicationUseCase: GetApplicationUseCase,
          private val listApplicationsUseCase: ListApplicationsUseCase,
          private val updateApplicationUseCase: UpdateApplicationUseCase
      ) {
          
          @PostMapping
          @Operation(summary = "Submit new application")
          @ApiResponses(
              ApiResponse(responseCode = "201", description = "Application created",
                  content = @Content(schema = @Schema(implementation = ApplicationResponse::class))),
              ApiResponse(responseCode = "400", description = "Invalid request"),
              ApiResponse(responseCode = "409", description = "Duplicate application")
          )
          fun submitApplication(
              @Valid @RequestBody request: SubmitApplicationRequest
          ): ResponseEntity<ApplicationResponse> {
              val command = request.toCommand()
              val result = submitApplicationUseCase.execute(command)
              return result.fold(
                  { error -> ResponseEntity.status(error.httpStatus).build() },
                  { application -> ResponseEntity.status(HttpStatus.CREATED).body(application.toResponse()) }
              )
          }
          
          @GetMapping("/{id}")
          @Operation(summary = "Get application by ID")
          fun getApplication(@PathVariable id: Long): ResponseEntity<ApplicationResponse> {
              val result = getApplicationUseCase.execute(id)
              return result.fold(
                  { error -> ResponseEntity.status(error.httpStatus).build() },
                  { ResponseEntity.ok(it.toResponse()) }
              )
          }
          
          @GetMapping
          @Operation(summary = "List applications with filters")
          fun listApplications(
              @Parameter(description = "Page number (0-based)") @RequestParam(defaultValue = "0") page: Int,
              @Parameter(description = "Page size") @RequestParam(defaultValue = "20") size: Int,
              @Parameter(description = "Filter by status") @RequestParam(required = false) status: ApplicationStatus?,
              @Parameter(description = "Filter by program") @RequestParam(required = false) programId: Long?
          ): ResponseEntity<Page<ApplicationSummaryResponse>> {
              val query = ListApplicationsQuery(page, size, status, programId)
              val result = listApplicationsUseCase.execute(query)
              return ResponseEntity.ok(result.map { it.toSummaryResponse() })
          }
          
          @PatchMapping("/{id}/status")
          @Operation(summary = "Update application status")
          fun updateStatus(
              @PathVariable id: Long,
              @Valid @RequestBody request: UpdateStatusRequest
          ): ResponseEntity<ApplicationResponse> {
              val command = UpdateApplicationStatusCommand(id, request.status, request.notes)
              val result = updateApplicationUseCase.execute(command)
              return result.fold(
                  { error -> ResponseEntity.status(error.httpStatus).build() },
                  { ResponseEntity.ok(it.toResponse()) }
              )
          }
      }
  - name: "Error Response Format"
    description: "Standardized error responses"
    code: |
      data class ApiErrorResponse(
          val timestamp: Instant = Instant.now(),
          val status: Int,
          val error: String,
          val message: String,
          val path: String,
          val details: Map<String, Any> = emptyMap()
      )
      
      @ControllerAdvice
      class GlobalExceptionHandler {
          
          @ExceptionHandler(MethodArgumentNotValidException::class)
          @ResponseStatus(HttpStatus.BAD_REQUEST)
          fun handleValidation(ex: MethodArgumentNotValidException, request: WebRequest): ApiErrorResponse {
              val details = ex.bindingResult.fieldErrors.associate {
                  it.field to it.defaultMessage ?: "Invalid value"
              }
              return ApiErrorResponse(
                  status = HttpStatus.BAD_REQUEST.value(),
                  error = "Validation Failed",
                  message = "Request validation failed",
                  path = request.getDescription(false),
                  details = details
              )
          }
          
          @ExceptionHandler(EntityNotFoundException::class)
          @ResponseStatus(HttpStatus.NOT_FOUND)
          fun handleNotFound(ex: EntityNotFoundException, request: WebRequest): ApiErrorResponse {
              return ApiErrorResponse(
                  status = HttpStatus.NOT_FOUND.value(),
                  error = "Not Found",
                  message = ex.message ?: "Resource not found",
                  path = request.getDescription(false)
              )
          }
      }