namespace eSport.TeamPlayer.API.GraphQL.Teams;

[ObjectType("Category")]
public class CategoryStub
{
    public int Id { get; set; }
   
}
// =========================================================================
// BƯỚC FIX QUAN TRỌNG: Đẩy Lookup ra tầng Root Query bằng [QueryType]
// =========================================================================
[QueryType] // Hoặc [ExtendObjectType(OperationTypeNames.Query)] tùy dự án của bạn
public partial class CategoryLookupQueries
{
    // Hàm này sẽ sinh ra ngoài tầng `type Query { categoryById(id: Int!): Category @lookup }`
    [Lookup]
    public CategoryStub? GetCategoryStubById(int id)
        => new() { Id = id };
}