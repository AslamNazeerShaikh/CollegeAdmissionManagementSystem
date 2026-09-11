---
name: kotlin-development
description: Modern Kotlin development practices and patterns
version: 1.0.0
author: opencode
tags:
  - kotlin
  - coroutines
  - sealed-classes
  - functional-programming
capabilities:
  - idiomatic-kotlin
  - coroutines-flow
  - sealed-classes
  - extension-functions
  - dsl-design
  - serialization
  - testing-with-mockk
references:
  - "https://kotlinlang.org/docs/home.html"
  - "https://kotlinlang.org/docs/coroutines-guide.html"
  - "https://github.com/mockk/mockk"
examples:
  - name: "Sealed Classes for State"
    description: "Model domain states with sealed classes"
    code: |
      sealed interface ApplicationState {
          data class Submitted(val application: Application) : ApplicationState
          data class UnderReview(val application: Application, val reviewerId: Long) : ApplicationState
          data class Approved(val application: Application, val approvalDate: LocalDateTime) : ApplicationState
          data class Rejected(val application: Application, val reason: String) : ApplicationState
          data class Waitlisted(val application: Application, val position: Int) : ApplicationState
      }
      
      fun processState(state: ApplicationState): String = when (state) {
          is ApplicationState.Submitted -> "Application submitted"
          is ApplicationState.UnderReview -> "Under review by ${state.reviewerId}"
          is ApplicationState.Approved -> "Approved on ${state.approvalDate}"
          is ApplicationState.Rejected -> "Rejected: ${state.reason}"
          is ApplicationState.Waitlisted -> "Waitlisted at position ${state.position}"
      }
  - name: "Coroutines & Flow"
    description: "Asynchronous programming with structured concurrency"
    code: |
      class ApplicationRepositoryImpl(
          private val dao: ApplicationDao,
          private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
      ) : ApplicationRepository {
          
          override suspend fun findById(id: Long): Result<Application?> =
              withContext(ioDispatcher) {
                  Result.runCatching { dao.findById(id)?.toDomain() }
              }
          
          override fun observeApplications(status: ApplicationStatus): Flow<List<Application>> =
              dao.observeByStatus(status)
                  .map { it.map { it.toDomain() } }
                  .flowOn(ioDispatcher)
          
          override suspend fun save(application: Application): Result<Application> =
              withContext(ioDispatcher) {
                  Result.runCatching {
                      val entity = application.toEntity()
                      dao.insert(entity)
                      entity.toDomain()
                  }
              }
      }
  - name: "Result Type for Error Handling"
    description: "Functional error handling without exceptions"
    code: |
      sealed interface Result<out T> {
          data class Success<out T>(val value: T) : Result<T>
          data class Failure(val error: Error) : Result<Nothing>
          
          companion object {
              inline fun <T> runCatching(block: () -> T): Result<T> =
                  try {
                      Success(block())
                  } catch (e: Exception) {
                      Failure(Error(e))
                  }
          }
          
          fun <R> map(transform: (T) -> R): Result<R> = when (this) {
              is Success -> Success(transform(value))
              is Failure -> this
          }
          
          fun <R> flatMap(transform: (T) -> Result<R>): Result<R> = when (this) {
              is Success -> transform(value)
              is Failure -> this
          }
          
          fun onSuccess(action: (T) -> Unit): Result<T> = apply { if (this is Success) action(value) }
          fun onFailure(action: (Error) -> Unit): Result<T> = apply { if (this is Failure) action(error) }
      }
      
      data class Error(val throwable: Throwable, val code: String? = null)