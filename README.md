# WalletAPI
Endpoint: POST /api/wallets/register/{playerId}
Description: Registers a new player.
Response:
200 OK: Player registered successfully.
409 Conflict: Player already registered.
 
Endpoint: PUT /api/wallets/transaction/{playerId}
Description: Processes a credit transaction for a player.
Request Body:
{
  "transactionId": "string",
  "type": "string",
  "amount": "decimal"
}
Response:
200 OK: Transaction accepted.
400 Bad Request: Transaction rejected.
404 Not Found: Player not found.

Endpoint: GET /api/wallets/balance/{playerId}
Description: Retrieves the player's wallet balance.
Response:
200 OK: Balance returned.
404 Not Found: Player not found.

Endpoint: GET /api/wallets/transactions/{playerId}
Description: Retrieves a player's transaction history.
Response:
200 OK: List of transactions.
404 Not Found: Player not found.


# WalletAPI

## Overview

This API allows for the management of player wallets, including registering new players, processing transactions, and retrieving wallet balances and transaction histories.

## Endpoints

### 1. Register Player

**Endpoint:**  
`POST /api/wallets/register/{playerId}`

**Description:**  
Registers a new player.

**Response:**
- `200 OK`: Player registered successfully.
- `409 Conflict`: Player already registered.

---

### 2. Credit Transaction

**Endpoint:**  
`PUT /api/wallets/transaction/{playerId}`

**Description:**  
Processes a credit transaction for a player.

**Request Body:**
```json
{
  "transactionId": "string",
  "type": "string",
  "amount": "decimal"
}```

**Response:**
200 OK: Transaction accepted.
400 Bad Request: Transaction rejected.
404 Not Found: Player not found.
