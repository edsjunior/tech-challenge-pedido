Feature: Pedido Management
  In order to manage pedidos
  As an API consumer
  I want to be able to create, read, update, and delete pedidos

Scenario: Create a new pedido
  Given I have a new pedido
  When I create the pedido
  Then the pedido should be created successfully
