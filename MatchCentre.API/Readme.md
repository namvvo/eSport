boundary:
Fixture  -> aggregateroot

# Football Subgraph

## Purpose
Subgraph chịu trách nhiệm dữ liệu bóng đá:

- Leagues
- Seasons
- Fixtures
- Teams
- Standings
- Match statistics

## Exposed entities
- Fixture
- Team
- League
- Season

## Consumed by
- Fusion Gateway
- Web Frontend
- Mobile API

## Technology
- .NET 10
- HotChocolate 16
- PostgreSQL
- EF Core 10