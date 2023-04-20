CREATE TABLE [dbo].[DatosGuardados] (
    [OperationID] INT           IDENTITY (1, 1) NOT NULL,
    [ValueOne]    NVARCHAR (50) NOT NULL,
    [Operator]    NVARCHAR (5)  NOT NULL,
    [ValueTwo]    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DatosGuardados] PRIMARY KEY CLUSTERED ([OperationID] ASC)
);

