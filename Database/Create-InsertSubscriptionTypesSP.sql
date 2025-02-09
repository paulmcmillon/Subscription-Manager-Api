-- ================================================================================================
-- Create susbcription types and related features for testing puposes.
-- These records would ideally be created with a "Seed" method on the dBContext
-- class in a ASP.NET Core project. 
-- ================================================================================================
ALTER PROCEDURE CreateSusbcriptionTypes
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

	-- Delete all records in the database and reset the identity
	-- cloumn (Id) seed to 1
	--TRUNCATE TABLE SubscriptionTypes		
	DELETE FROM [SubscriptionTypes];	
	DBCC CHECKIDENT ('SubscriptionTypes', RESEED, 0);
	

-- INSERT LIGHT [SubscriptionType] AND RELATED [Features]
	INSERT INTO [SubscriptionTypes](
	 [Name]
	,[Description]
	,[PriceMonthly]
	,[PriceYearly]
	)
	VALUES(
		'GroundzKeeper Light'
		,'Description for a basic level subscription'
		,9.99
		,99.99
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		1
		,'Customer Management'
		,'Description 1'
	);
	
	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		1
		,'Billing'
		,'Description 2'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		1
		,'Job Scheduling'
		,'Description 3'
	);

-- INSERT PRO [SubscriptionType] AND RELATED [Features]
	INSERT INTO [SubscriptionTypes](
	 [Name]
	,[Description]
	,[PriceMonthly]
	,[PriceYearly]
	)
	VALUES(
		'GroundzKeeper Pro'
		,'Description for a pro level subscription'
		,49.99
		,539.99
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		2
		,'Customer Management'
		,'Description 1'
	);
	
	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		2
		,'Billing'
		,'Description 2'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		2
		,'Job Scheduling'
		,'Description 3'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		2
		,'Inventory'
		,'Description 4'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		2
		,'Mobile App'
		,'Description 5'
	);


-- INSERT ENTERPRISE [SubscriptionType] AND RELATED [Features]
INSERT INTO [SubscriptionTypes](
	 [Name]
	,[Description]
	,[PriceMonthly]
	,[PriceYearly]
	)
	VALUES(
		'GroundzKeeper Enterprise'
		,'Description for a enterprise level subscription'
		,99.99
		,999.99
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Customer Management'
		,'Description 1'
	);
	
	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Billing'
		,'Description 2'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Job Scheduling'
		,'Description 3'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Inventory'
		,'Description 4'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Mobile App'
		,'Description 5'
	);

	INSERT INTO [Features](
	 [Features].[SubscriptionTypeId]
	,[Features].[Name]
	,[Features].[Description]
	)
	VALUES(
		3
		,'Reporting'
		,'Description 6'
	);
END
GO
