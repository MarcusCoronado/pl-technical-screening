/*
CREATE TABLE [dbo].[StatusChanges](
    [TicketId] [int] NULL,
    [Timestamp] [datetime2](7) NULL,
    [OldStatus] [varchar](50) NULL,
    [NewStatus] [varchar](50) NULL
);

CREATE TABLE [dbo].[Tickets](
    [Id] [int] NULL,
    [Summary] [varchar](2000) NULL
);

INSERT [dbo].[StatusChanges] ([TicketId], [Timestamp], [OldStatus], [NewStatus])
    VALUES
         (1, CAST(N'2021-01-01T00:00:00.0000000' AS DateTime2), NULL, N'New')
        ,(1, CAST(N'2021-01-01T01:15:00.0000000' AS DateTime2), N'New', N'In Progress')
        ,(2, CAST(N'2021-01-01T02:32:00.0000000' AS DateTime2), NULL, N'New')
        ,(3, CAST(N'2021-01-01T04:53:00.0000000' AS DateTime2), NULL, N'New')
        ,(2, CAST(N'2021-01-01T05:07:00.0000000' AS DateTime2), N'New', N'In Progress')
        ,(2, CAST(N'2021-01-01T06:14:00.0000000' AS DateTime2), N'In Progress', N'Closed')
        ,(4, CAST(N'2021-01-01T07:22:00.0000000' AS DateTime2), NULL, N'New')
        ,(1, CAST(N'2021-01-01T08:25:00.0000000' AS DateTime2), N'In Progress', N'Closed')
        ,(2, CAST(N'2021-01-01T09:32:00.0000000' AS DateTime2), N'Closed', N'Reopened')
        ,(3, CAST(N'2021-01-01T10:55:00.0000000' AS DateTime2), N'New', N'In Progress')
        ,(4, CAST(N'2021-01-01T11:05:00.0000000' AS DateTime2), N'New', N'Closed');

INSERT [dbo].[Tickets] ([Id], [Summary])
    VALUES
         (1, N'My Computer wont turn')
        ,(2, N'Could you print the date out in my timezone on Report XYZ?')
        ,(3, N'Could you create a new version of the XYZ report that has 2 new columns?')
        ,(4, N'Where is the password reset link?');
GO
*/


CREATE OR ALTER PROCEDURE [dbo].[CurrentOpenTickets]
    @EndDate DateTime2 = NULL
AS BEGIN
    SET @EndDate = IsNull(@EndDate, GetDate());

    WITH [statuses_cte] ([TicketId], [Status], [MinutesSpent], [NextStatus]) AS (
        SELECT
             sOld.[TicketId]
            ,sOld.[NewStatus] 'Status'
            ,DateDiff(
                 MINUTE
                ,sOld.[Timestamp]
                ,CASE WHEN sNew.[Timestamp] IS NULL    THEN @endDate
                      WHEN sNew.[Timestamp] > @endDate THEN @endDate
                      ELSE sNew.[Timestamp] END
            ) 'MinutesSpent'
            ,sNew.[NewStatus] 'NextStatus'
        FROM
                      [dbo].[StatusChanges] sOld
            LEFT JOIN [dbo].[StatusChanges] sNew ON  sNew.[TicketId]  = sOld.[TicketId]
                                                 AND sNew.[OldStatus] = sOld.[NewStatus]
        WHERE
              (sOld.[Timestamp] <= @endDate)
     )
    SELECT
         [Id]
        ,[Summary]
        ,MAX(CASE [Status] WHEN 'New'         THEN [MinutesSpent] END) 'New'
        ,MAX(CASE [Status] WHEN 'In Progress' THEN [MinutesSpent] END) 'In Progress'
        ,MAX(CASE [Status] WHEN 'Closed'      THEN [MinutesSpent] END) 'Closed'
        ,MAX(CASE [Status] WHEN 'Reopened'    THEN [MinutesSpent] END) 'Reopened'
    FROM
                  [dbo].[Tickets]  t
        LEFT JOIN [statuses_cte]   s ON s.[TicketId] = t.[Id]
    GROUP BY
        [Id], [Summary]
    ORDER BY
        [Id];

END;
GO


-- EXEC [dbo].[CurrentOpenTickets] N'2020-12-31T00:00:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-01T12:00:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-01T00:00:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-01T02:33:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-01T06:14:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-02T11:05:00'
-- EXEC [dbo].[CurrentOpenTickets] N'2021-01-03T11:05:00'
-- EXEC [dbo].[CurrentOpenTickets]