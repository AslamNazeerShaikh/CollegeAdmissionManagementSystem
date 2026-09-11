---
name: gradle-build
description: Gradle build system expertise for Kotlin/Java projects
version: 1.0.0
author: opencode
tags:
  - gradle
  - kotlin
  - build
  - kotlin-dsl
capabilities:
  - build-configuration
  - dependency-management
  - multi-module-setup
  - plugin-management
  - build-optimization
  - custom-tasks
  - version-catalogs
references:
  - "https://docs.gradle.org/current/userguide/userguide.html"
  - "https://docs.gradle.org/current/userguide/kotlin_dsl.html"
  - "https://github.com/gradle/gradle"
examples:
  - name: "Configure Kotlin DSL build"
    description: "Set up a modern Gradle project with Kotlin DSL"
    code: |
      // build.gradle.kts
      plugins {
          kotlin("jvm") version "1.9.0"
          kotlin("plugin.serialization") version "1.9.0"
          id("com.google.dagger.hilt") version "2.48" apply false
          id("io.gitlab.arturbosch.detekt") version "1.23.0"
          id("org.jlleitschuh.gradle.ktlint") version "11.0.0"
      }
      
      group = "com.college.admission"
      version = "1.0.0-SNAPSHOT"
      
      repositories {
          mavenCentral()
          google()
      }
      
      dependencies {
          implementation(libs.kotlin.stdlib)
          implementation(libs.coroutines.core)
          implementation(libs.ktor.server.core)
          testImplementation(libs.junit.jupiter)
          testImplementation(libs.mockk)
      }
      
      tasks.withType<Test> {
          useJUnitPlatform()
      }
  - name: "Version Catalog"
    description: "Centralize dependency versions"
    code: |
      # gradle/libs.versions.toml
      [versions]
      kotlin = "1.9.0"
      coroutines = "1.7.3"
      ktor = "2.3.8"
      junit = "5.10.1"
      mockk = "1.13.12"
      
      [libraries]
      kotlin-stdlib = { module = "org.jetbrains.kotlin:kotlin-stdlib", version.ref = "kotlin" }
      coroutines-core = { module = "org.jetbrains.kotlinx:kotlinx-coroutines-core", version.ref = "coroutines" }
      ktor-server-core = { module = "io.ktor:ktor-server-core", version.ref = "ktor" }
      junit-jupiter = { module = "org.junit.jupiter:junit-jupiter", version.ref = "junit" }
      mockk = { module = "io.mockk:mockk", version.ref = "mockk" }
      
      [plugins]
      kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
      detekt = { id = "io.gitlab.arturbosch.detekt", version.ref = "detekt" }
      ktlint = { id = "org.jlleitschuh.gradle.ktlint", version.ref = "ktlint" }