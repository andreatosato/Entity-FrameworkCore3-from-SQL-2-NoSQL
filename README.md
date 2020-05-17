# Entity Framework Core v3 from SQL to NoSQL

## Scaletta:

| Tabella | Tabella |
| - | - |
|1 Studente | N Esami |
|1 Corso | N Esami |

Indirizzo (Owned Entity)
Studente (Owned Entity esempio).

### Azione:
Cambiare la mail universitaria di uno studente.

### Enum (ValueConverter)
 - [x] Corso Obbligatorio
 - [x] Corso Facoltativo
 
#### Crediti Extra 
 (Nome, Crediti, Numero ore)
 Value Converter (partecipazione a convegni o attività sociali) 

# EF Core SQL Server
1. Entity Configuration
    * Configurare il DbContext
2. Migrations
3. Include (.NET 5)
4. ValueConverter automatico Enum to String (Anche con JSON Field)
5. Owned Entity
6. Utilizzare il client nativo 
7. Retry
8. Transazioni

# EF Core Cosmos
1. Entity Configuration
    * Configurare il DbContext
2. Migrations
3. Include (no join)
4. ValueConverter automatico Enum to String
5. Owned Entity
6. Utilizzare il client nativo 
7. Retry (ConnectionPolicy.RetryOptions) ?
8. Transazioni
