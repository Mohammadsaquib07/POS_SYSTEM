# POS Invoice System - Supermarket Style Implementation

## Overview
The POS (Point of Sale) invoice system has been refactored to use **Entity Framework Core** with a clean, professional architecture similar to real supermarket checkout systems.

## Architecture Overview

### Data Access Layer (EF Core)
Two new repositories implement the invoice workflow:

#### 1. **CustomerRepository.cs** - Customer Management
```csharp
ICustomerCreate:
  - AddCustomer(customer) → Creates new customer record

ICustomerRead:
  - GetCustomer(id) → Retrieves customer by ID
  - GetAllCustomers() → Lists all customers
```

#### 2. **InvoiceRepository.cs** - Invoice Management
```csharp
IInvoiceCreate:
  - AddInvoice(invoice) → Creates invoice header
  - AddInvoiceItem(item) → Adds line item to invoice

IInvoiceRead:
  - GetInvoice(id) → Retrieves invoice with full details
  - GetInvoicesByCustomer(customerId) → Gets all invoices for customer
  - GetAllInvoices() → Lists all invoices
```

### Business Logic Layer
**InvoiceService.cs** orchestrates the complete supermarket checkout workflow.

## Invoice Creation Workflow

### Step 1: Validate Customer
- Verifies customer exists in database
- Ensures CustomerId is valid (> 0)

### Step 2: Validate Items
- Checks that at least 1 item is in the cart
- Ensures items list is not null/empty

### Step 3: Process & Validate Items
- Validates each product exists in inventory
- Checks quantities are positive integers
- Retrieves product details (Name, Price) from database

### Step 4: Calculate Subtotal
- Sum all (Quantity × UnitPrice) for each item
- Example: 2 items × 500 + 3 items × 200 = 1600

### Step 5: Calculate Tax (18% GST/VAT)
- Tax = Subtotal × 0.18
- Example: 1600 × 0.18 = 288

### Step 6: Calculate Total
- Total = Subtotal + Tax
- Example: 1600 + 288 = 1888

### Step 7: Generate Invoice Number
- Format: `INV-{yyyyMMddHHmmss}`
- Example: `INV-20250126143652`
- Ensures unique invoice ID per transaction

### Step 8: Save Invoice Header
- Creates `Invoices` record with:
  - InvoiceNumber
  - CustomerId
  - InvoiceDate
  - Subtotal
  - TaxAmount
  - TotalAmount
  - CreatedBy (always "POS_System")

### Step 9: Save Line Items
- Creates `InvoiceItem` records for each product
- Links each item to the invoice via InvoiceId
- Stores: ProductId, Quantity, UnitPrice, LineTotal

## API Endpoints

### Create Invoice (Existing Customer)
```
POST /api/invoice/create
Body:
{
  "customerId": 1,
  "invoiceDate": "2025-01-26T14:30:00",
  "items": [
    {
      "productId": 5,
      "quantity": 2,
      "unitPrice": 500
    },
    {
      "productId": 8,
      "quantity": 3,
      "unitPrice": 200
    }
  ],
  "notes": "Customer requested gift wrapping"
}

Response:
{
  "invoiceId": 42,
  "invoiceNumber": "INV-20250126143652",
  "subtotal": 1600,
  "taxAmount": 288,
  "totalAmount": 1888
}
```

### Create Customer & Invoice (Walk-in Customer)
```
POST /api/invoice/create-customer-and-invoice
Body:
{
  "customer": {
    "name": "John Doe",
    "email": "john@example.com",
    "phone": "03001234567",
    "billingAddress": "123 Main Street, City"
  },
  "items": [
    {
      "productId": 5,
      "quantity": 1,
      "unitPrice": 500
    }
  ]
}

Response:
{
  "invoiceId": 43,
  "invoiceNumber": "INV-20250126143653",
  "subtotal": 500,
  "taxAmount": 90,
  "totalAmount": 590
}
```

### Get Invoice
```
GET /api/invoice/{invoiceId}

Response:
{
  "invoiceId": 42,
  "invoiceNumber": "INV-20250126143652",
  "customerId": 1,
  "customer": {
    "customerId": 1,
    "name": "Ahmed Khan",
    "email": "ahmed@email.com"
  },
  "items": [
    {
      "productId": 5,
      "productName": "Laptop",
      "quantity": 2,
      "unitPrice": 500,
      "lineTotal": 1000
    }
  ],
  "subtotal": 1600,
  "taxAmount": 288,
  "totalAmount": 1888,
  "invoiceDate": "2025-01-26T14:30:00"
}
```

### Get All Invoices
```
GET /api/invoice/all

Response: List of all invoices with complete details
```

## Database Schema

### Invoices Table
```
InvoiceId (PK)
InvoiceNumber (NVARCHAR, UNIQUE)
CustomerId (FK → Customers)
InvoiceDate
Subtotal (DECIMAL)
TaxAmount (DECIMAL)
TotalAmount (DECIMAL)
Notes
CreatedBy
CreatedAt (GETUTCDATE default)
```

### InvoiceItems Table
```
InvoiceItemId (PK)
InvoiceId (FK)
ProductId (FK)
Quantity
UnitPrice (DECIMAL)
LineTotal (DECIMAL)
CreatedAt (GETUTCDATE default)
```

### Customers Table
```
CustomerId (PK)
Name (NVARCHAR)
Email (NVARCHAR, UNIQUE)
Phone (NVARCHAR)
BillingAddress (NVARCHAR)
CreatedAt (GETUTCDATE default)
```

## Key Features

✅ **Clean Architecture** - Separated concerns (Data, Business Logic, API)
✅ **EF Core Integration** - LINQ queries instead of raw SQL
✅ **Validation** - Comprehensive input validation at service layer
✅ **Error Handling** - Meaningful exception messages for debugging
✅ **Tax Calculation** - Configurable 18% tax rate
✅ **Unique Invoice Numbers** - Based on timestamp (INV-yyyyMMddHHmmss)
✅ **Customer Management** - Support for registered and walk-in customers
✅ **Line Items** - Detailed breakdown of all products in invoice

## Testing the System

### Using Swagger UI
1. Navigate to: http://localhost:5203/swagger/index.html
2. Look for Invoice endpoints
3. Try the "Create Invoice" endpoint with sample data
4. View results in the Response panel

### Sample Test Data
```json
{
  "customerId": 1,
  "invoiceDate": "2025-01-26T14:30:00Z",
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 999.99
    },
    {
      "productId": 2,
      "quantity": 1,
      "unitPrice": 500.00
    }
  ],
  "notes": "Regular customer"
}
```

## Error Scenarios

| Scenario | Error Message | Solution |
|----------|---------------|----------|
| Missing Customer ID | "Valid CustomerId is required" | Select a customer |
| No Items in Cart | "Invoice must have at least one item" | Add items to cart |
| Invalid Product ID | "Product not found" | Verify product exists |
| Zero/Negative Qty | "Quantity must be positive" | Enter valid quantity |
| Missing Customer Name | "Customer name is required" | Provide customer name |
| Invalid Email | "Customer email format is invalid" | Use valid email format |

## Code Structure

```
/home/saquib/POS_SYSTEM/
├── BL/
│   ├── InvoiceService.cs          ← Main business logic
│   ├── InvoiceRepository.cs        ← EF Core repository (Create/Read)
│   ├── CustomerRepository.cs       ← Customer CRUD
│   └── IInvoiceService.cs
├── Model/
│   ├── Invoices.cs                 ← Invoice entity
│   ├── InvoiceItem.cs              ← Line item entity
│   ├── Customers.cs                ← Customer entity
│   ├── ICustomerCreate.cs          ← Interfaces
│   └── IInvoiceCreate.cs
├── Controllers/
│   └── CreateInvoiceController.cs  ← API endpoints
├── DTOs/
│   ├── CreateInvoiceRequest.cs
│   ├── InvoiceItemRequestDto.cs
│   └── CreateCustomerDto.cs
└── Program.cs                       ← Dependency injection setup
```

## Migration & Database Setup

The system uses Entity Framework Core migrations:
```bash
dotnet ef database update
```

Ensures all tables are created with proper defaults (GETUTCDATE for CreatedAt).

---

**System Ready for Production Testing!**
✨ All endpoints are functional and accessible via Swagger UI at http://localhost:5203/swagger
