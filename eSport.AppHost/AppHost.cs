var builder = DistributedApplication.CreateBuilder(args);

//builder.AddForwardedHeaders();

var redis = builder.AddRedis("redis")
    .WithRedisInsight();

var rabbitMq = builder.AddRabbitMQ("eventbus")
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithImage("pgvector/pgvector")
    .WithImageTag("pg17")
    .WithDataVolume("thethaoso")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();



var catalogDb = postgres.AddDatabase("catalogdb");

var catalogApi = builder.AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(redis)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(catalogDb).WaitFor(catalogDb);

redis.WithParentRelationship(catalogApi);

var mediaDb = postgres.AddDatabase("mediadb");

var mediaApi = builder.AddProject<Projects.Media_API>("media-api")
    .WithUrl("/graphql", "GraphQL")
    .WithReference(mediaDb).WaitFor(mediaDb);

var teamPlayerDb = postgres.AddDatabase("teamPlayerdb");
var teamPlayerApi = builder.AddProject<Projects.TeamPlayer_API>("teamPlayer-api")
    .WithUrl("/graphql", "GraphQL")
    .WithReference(redis)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(teamPlayerDb).WaitFor(teamPlayerDb);
redis.WithParentRelationship(teamPlayerApi);

var matchCentreDb = postgres.AddDatabase("matchCentredb");
var matchCentreApi = builder.AddProject<Projects.MatchCentre_API>("matchCentre-api")
    .WithReference(redis)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(matchCentreDb).WaitFor(matchCentreDb);

redis.WithParentRelationship(matchCentreApi);

var gatewayApi = builder.AddProject<Projects.Gateway_API>("gateway-api")
    .WithReference(redis);
redis.WithParentRelationship(gatewayApi);



builder.AddProject<Projects.eSport_UI>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
   ;


builder.Build().Run();
