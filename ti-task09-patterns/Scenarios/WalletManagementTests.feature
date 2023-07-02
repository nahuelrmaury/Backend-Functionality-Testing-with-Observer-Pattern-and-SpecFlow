Feature: WalletManagementTests

As a user
I want to manage my wallet effectively
So that I can keep track of my financial transactions and balances

Scenario: 01 Get balance after new user is created
	Given Create new user
	When Get balance
	Then Status code is 'InternalServerError'

Scenario: 02 Get balance from not active user
	Given Create new user
	When Get balance
	Then Status code is 'InternalServerError'

Scenario: 03 Get balance from not existing user
	Given Get id from not existing user
	When Get balance
	Then Status code is 'InternalServerError'

Scenario: 04 Get balanca from user without transactions
	Given Create new user
	And Make user active
	When Get transactions
	Then Status code is 'OK'

Scenario: 05 Get balance after revert transaction
	Given Create new user
	And Make user active
	And Make charge to wallet
	When Revert transaction
	And Get balance
	Then Status code is 'OK'

Scenario: 06 Carge wallet to not active user
	Given Create new user
	When Make charge to wallet
	And Get balance
	Then Status code is 'InternalServerError'
	
Scenario: 07 Charge wallet with balance so it end with negative balance
	Given Create new user
	And Make user active
	And Make charge to wallet
	When Charge wallet with more than it already have
	Then Status code is 'InternalServerError'

Scenario: 08 Charge wallet with balance so it end with 0 balance
	Given Create new user
	And Make user active
	And Make charge to wallet
	When Charge wallet with negative balance amount
	Then Status code is 'OK'

Scenario: 09 Charge wallet with minus N and charge again with N
	Given Create new user
	And Make user active
	And Make charge to wallet
	When Charge wallet with negative balance amount
	Then Status code is 'OK'

Scenario: 10 Charge wallet so it end with negative balance
	Given Create new user
	And Make user active
	When Make negative charge
	Then Status code is 'InternalServerError'

Scenario: 11 Charge wallet with balance plus ten and charge it again
	Given Create new user
	And Make user active
	And Make charge to wallet plus ten
	When Make charge to wallet
	Then Status code is 'OK'