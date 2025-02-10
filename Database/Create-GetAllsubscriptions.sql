
CREATE PROCEDURE GetAllSubscriptions

AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    SELECT s.Id
	,st.Name
	,s.AdministratorName
	,s.Email
	,s.PurchaseDate
	,s.NextPaymentDate
	,s.LockReason
	FROM [Subscriptions] s INNER JOIN [SubscriptionTypes] st ON s.SubscriptionTypeId = st.Id
END
GO
