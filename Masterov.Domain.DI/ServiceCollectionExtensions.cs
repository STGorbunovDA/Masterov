using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerOrders;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Masterov.UserFolder.LoginUser;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Domain.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        //FinishedProduct
        services
            .AddScoped<IGetFinishedProductByIdUseCase, GetFinishedProductByIdUseCase>()
            .AddScoped<IGetFinishedProductByNameUseCase, GetFinishedProductByNameUseCase>()
            .AddScoped<IDeleteFinishedProductUseCase, DeleteFinishedProductUseCase>()
            .AddScoped<IAddFinishedProductUseCase, AddFinishedProductUseCase>()
            .AddScoped<IUpdateFinishedProductUseCase, UpdateFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductOrdersUseCase, GetFinishedProductOrdersUseCase>()
            .AddScoped<IGetFinishedProductsUseCase, GetFinishedProductsUseCase>();

        //ProductionOrder
        services
            .AddScoped<IGetProductionOrdersUseCase, GetProductionOrdersUseCase>()
            .AddScoped<IGetProductionOrdersByCreatedAtUseCase, GetProductionOrdersByCreatedAtUseCase>()
            .AddScoped<IGetProductionOrdersByCompletedAtUseCase, GetProductionOrdersByCompletedAtUseCase>()
            .AddScoped<IGetProductionOrdersByDescriptionUseCase, GetProductionOrdersByDescriptionUseCase>()
            .AddScoped<IGetProductionOrdersByStatusUseCase, GetProductionOrdersByStatusUseCase>()
            .AddScoped<IGetFinishedProductAtOrderUseCase, GetFinishedProductAtOrderUseCase>()
            .AddScoped<IGetProductComponentAtOrderUseCase, GetProductComponentAtOrderUseCase>()
            .AddScoped<IGetProductionOrderByIdUseCase, GetProductionOrderByIdUseCase>();
        
        // ProductType
        services
            .AddScoped<IUpdateProductTypeUseCase, UpdateProductTypeUseCase>()
            .AddScoped<IDeleteProductTypeUseCase, DeleteProductTypeUseCase>()
            .AddScoped<IGetProductTypeByNameUseCase, GetProductTypeByNameUseCase>()
            .AddScoped<IAddProductTypeUseCase, AddProductTypeUseCase>()
            .AddScoped<IGetProductTypeByIdUseCase, GetProductTypeByIdUseCase>()
            .AddScoped<IGetProductsTypeUseCase, GetProductsTypeUseCase>();
        
        //users
        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<ILoginUserUseCase, LoginUserUseCase>()
            .AddScoped<IGetUserByLoginUseCase, GetUserByLoginUseCase>()
            .AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>()
            .AddScoped<IDeleteUserByLoginUseCase, DeleteUserByLoginUseCase>()
            .AddScoped<IDeleteUserByIdUseCase, DeleteUserByIdUseCase>()
            .AddScoped<IChangeRoleUserUseCase, ChangeRoleUserUseCase>()
            .AddScoped<IGetUsersUseCase, GetUsersUseCase>();
        
        //customer
        services
            .AddScoped<IAddCustomerUseCase, AddCustomerUseCase>()
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()
            .AddScoped<IGetCustomerByNameUseCase, GetCustomerByNameUseCase>()
            .AddScoped<IGetCustomerOrdersUseCase, GetCustomerOrdersUseCase>()
            .AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>()
            .AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>()
            .AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
        
        //payment
        services
            .AddScoped<IGetPaymentsUseCase, GetPaymentsUseCase>();
            
        return services;
    }
}