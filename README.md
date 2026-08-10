The project uses the gRPC, GraphQL gateway fusion to expose the data to frontend

             Client
                │
          GraphQL Gateway (Fusion)
         /          |            \
    Catalog     TeamPlayer     MatchCentre
         ▲             ▲
         │             │
       gRPC <-------> gRPC
