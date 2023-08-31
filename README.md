# Backend Functionality Testing with Observer Pattern

This repository contains a task focused on testing the functionality of backend services using the Observer pattern. Specifically, it involves testing the methods of the UserService (excluding CacheManagement) and the /Charge and /GetBalance endpoints of the WalletService while implementing the Observer pattern for monitoring changes.

## Table of Contents
- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Task Description](#task-description)
- [Testing Methodology](#testing-methodology)
- [Running the Tests](#running-the-tests)
- [Test Results](#test-results)

## Introduction

Testing the backend functionality of an application is crucial to ensure that it works as expected and meets the requirements. In this task, we not only test the functionality of the services but also implement the Observer pattern to monitor and react to changes in real-time.

## Prerequisites

To complete this task, you will need:

- Access to the UserService and WalletService.
- A testing tool or framework for sending HTTP requests (e.g., Postman, Swagger UI, or a testing library like `requests` in Python).
- An understanding of the methods and endpoints you are testing.
- Knowledge of the Observer pattern and how to implement it in your programming language.

## Task Description

### Task 1: Testing UserService Methods

1. Access the Swagger documentation for the UserService.

2. Test all the methods of the UserService except for CacheManagement. Ensure that each method performs as expected and returns the correct responses.

3. Implement the Observer pattern to monitor changes or events in the UserService. For example, you could observe user creation, deletion, or updates.

### Task 2: Testing WalletService Endpoints

1. Access the Swagger documentation for the WalletService.

2. Test the `/Charge` and `/GetBalance` endpoints of the WalletService. Verify that these endpoints handle requests correctly and return the expected results.

3. Implement the Observer pattern to monitor changes or events in the WalletService. For example, you could observe balance changes or transactions.

## Testing Methodology

When testing each method or endpoint and implementing the Observer pattern, consider the following:

- Test various input scenarios, including valid inputs, invalid inputs, edge cases, and boundary conditions.
- Verify the responses, including status codes, response payloads, and error handling.
- Check for proper authentication and authorization if applicable.
- Implement observers that can react to changes in real-time. For example, you could log changes, send notifications, or trigger actions.

## Running the Tests

1. Clone this repository to your local machine.

2. Open your preferred testing tool or framework.

3. Create test cases or requests for each method or endpoint you are testing.

4. Execute the tests, and observe the results.

## Test Results

Document the results of your tests, including any issues or unexpected behavior encountered during testing. Additionally, document the behavior of the Observer pattern in response to changes in the services.
