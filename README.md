# Application Architecture in C# .NET

This repository builds on a banking application developed in Object-Oriented Programming at Deakin University, by exploring different approaches to application architecture and design.

## Contents

* 01_BaseBankingApplication
    * Code as submitted during the university subject

* 02_ClearArchitecture
    * Re-organisation of the base application
    * Essentially equivalent functionality

* 03_CleanArchitecture_WebApi
    * Extension of 02 to replace the UI
    * WebAPI added for presentation
    * CLI has been removed, but this is optional. Both can be used simultaneously
    * To persist data for the lifetime of the program:
        * List<Account> _accounts moved out of AccountRepository
        * List<Transaction> _transactions moved out of TransactionRepository
        * static FakeDB created that now contains the lists

* 04_CleanArchitecture_SQL
    * Extension of 03 to replace the static FakeDB with a SQL Server database
    * Utilises Entity Framework
    * Although Entity Framework implements the Repository Pattern internally, it has been explicitly implemented here as part of exploring architectural approaches