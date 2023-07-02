Feature: User Management

  As a system administrator
  I want to be able to create, read, update and delete users
  In order to manage users

Scenario: 01 Create user with empty fields
	When New user is created with empty fields
	Then Response code is 'OK'

Scenario: 02 Create user with null fields
	When New user is created with null fields
	Then Response code is 'InternalServerError'

Scenario: 03 Create user with digits in fields
	When New user is created with random digits
	Then Response code is 'OK'

Scenario: 04 Create user with special chars in fields
	When New user is created with random specials chars
	Then Response code is 'OK'

Scenario: 05 Create user with diferent length
	When New user is created with one length
	Then Response code is 'OK'

Scenario: 06 Create user with diferent length
	When New user is created with one hundred one length
	Then Response code is 'OK'

Scenario: 07 Create two users and compare content
	Given New users are created
	When Compare content of users
	Then Content value is autoincremented
	
Scenario: 08 Create two users, delete second user and create another user 
	Given New users are created
	And Second user is deleted
	And New user is created
	When Compare content of users
	Then Content value is autoincremented

Scenario: 09 Get user status of not existing user 
	Given Get random user id
	When Get status from user
	Then Response code is 'InternalServerError'

Scenario: 10 Get user status of valid user
	Given New user is created
	When Get status from user
	Then Status is 'false'

Scenario: 11 Get user status after changing it to false
	Given New user is created
	When Change status to 'false'
	And Get status from user
	Then Status is 'false'

Scenario: 12 Get user status after changing it to true
	Given New user is created
	When Change status to 'true'
	And Get status from user
	Then Status is 'true'

Scenario: 13 Get status code after set user status to not existing user
	Given Get random user id
	When Change status to 'true'
	And Get status from user
	Then Response code is 'InternalServerError'

Scenario: 14 Get user status after changing it from false to true
	Given New user is created
	When Change status to 'true'
	And Get status from user
	Then Status is 'true'

Scenario: 15 Get user status after changing it from false to true and false again
	Given New user is created
	When Change status to 'true'
	And Change status to 'false'
	And Get status from user
	Then Status is 'false'

Scenario: 16 Get user status after changing it from false, to true, to false and to true again
	Given New user is created
	When Change status to 'true'
	And Change status to 'false'
	And Change status to 'true'
	And Get status from user
	Then Status is 'true'

Scenario: 17 Get user status after changing it from false to false
	Given New user is created
	When Change status to 'false'
	And Get status from user
	Then Status is 'false'

Scenario: 18 Get user status after changing it from false, to true, to false and to true again
	Given New user is created
	When Change status to 'true'
	And Change status to 'false'
	And Change status to 'true'
	And Get status from user
	Then Status is 'true'

Scenario: 19 Delete not active user
	Given New user is created
	When Delete user
	Then Response code is 'OK'

Scenario: 20 Delete not existing user
	Given Get random user id
	When Delete user
	Then Response code is 'InternalServerError'