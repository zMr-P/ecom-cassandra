using Cassandra;
using Cassandra.Fluent.Migrator.Core;

namespace ecom_cassandra.Infrastructure.Migrations;

public class CreateOrderProductOrderItemReviewCategory20260203(ISession session) : IMigrator
{
    public string Name => "CreateOrderProductOrderItemReviewCategory20260203";
    public Version Version => new Version(1, 0, 1);
    public string Description => "Create tables orders, order_items, products, reviews and categories";
    private readonly ISession _session = session;
    
    public async Task ApplyMigrationAsync()
    {
        const string createOrderTableCql = @"
            CREATE TABLE orders (
                id uuid PRIMARY KEY,
                user_id uuid,
                status text,
                total_amount decimal,
                created_at timestamp,
                updated_at timestamp,
                shipping_address frozen<address>
        );";
        await _session.ExecuteAsync(new SimpleStatement(createOrderTableCql));
        
        const string createOrderItemsTableCql = @"
            CREATE TABLE order_items (
                order_id uuid,
                product_id uuid,
                quantity int,
                unit_price decimal,
                PRIMARY KEY (order_id, product_id)
        );";
        
        await _session.ExecuteAsync(new SimpleStatement(createOrderItemsTableCql));
        
        const string createCategoryTableCql = @"
            CREATE TABLE categories (
                id uuid PRIMARY KEY,
                name text,
                description text
        );";
        
        await _session.ExecuteAsync(new SimpleStatement(createCategoryTableCql));
        
        const string createProductTableCql = @"
            CREATE TABLE products (
                id uuid PRIMARY KEY,
                category_id uuid,
                name text,
                description text,
                price decimal,
                stock_quantity int,
                created_at timestamp,
                updated_at timestamp
        );";
        
        await _session.ExecuteAsync(new SimpleStatement(createProductTableCql));
        
        const string createReviewTableCql = @"
            CREATE TABLE reviews (
                id uuid PRIMARY KEY,
                product_id uuid,
                user_id uuid,
                rating int,
                comment text,
                created_at timestamp
        );";
        
        await _session.ExecuteAsync(new SimpleStatement(createReviewTableCql));
    }
}