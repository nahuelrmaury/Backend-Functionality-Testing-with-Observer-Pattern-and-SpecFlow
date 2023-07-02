Feature: WalletManagementTests

As a user
I want to manage my wallet effectively
So that I can keep track of my financial transactions and balances

Scenario: Get balance after new user is created
	Given Create new user
	When Get balance
	Then Status code is 'InternalServerError'
