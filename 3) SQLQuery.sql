SELECT Invoices.id, Invoices.BillingDate, Customers.Name, Customers.ReferredBy FROM Invoices
FULL OUTER JOIN Customers ON
Invoices.CustomerId = Customers.Id  ORDER BY BillingDate DESC

