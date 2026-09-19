# MiniECommerce

Small Modular Monolith + Clean Architecture e-commerce project.

## Modules
- Identity
- Catalog
- Inventory
- Cart
- Orders
- Payments
- Reviews
- Notifications

## Each module
API
Application
Domain
Infrastructure

## Intended dependency direction
API -> Application -> Domain
Infrastructure -> Application + Domain

Host is the composition root.

This package contains architecture/project structure only.
No business implementation is included.
