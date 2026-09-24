# Inventory Module

MiniECommerce Inventory module following the same 4-layer module structure:

- API
- Application
- Domain
- Infrastructure

## Main responsibility

Inventory owns product stock. Catalog owns product information.

## Main entity

`ProductInventory`

- ProductId
- Quantity
- ReservedQuantity

Available quantity is calculated as:

`Quantity - ReservedQuantity`

The module intentionally does not create a database foreign key to Catalog's Product because modules own their persistence boundaries.
