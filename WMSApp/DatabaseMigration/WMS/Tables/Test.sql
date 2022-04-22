CREATE TABLE [WMS].[Test] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [TestName] NVARCHAR (50) NULL
);


GO
CREATE NONCLUSTERED INDEX [TestOnTestName]
    ON [WMS].[Test]([TestName] ASC);

