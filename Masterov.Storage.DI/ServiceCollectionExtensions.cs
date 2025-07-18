using System.Reflection;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.Payment.AddPayment;
using Masterov.Domain.Masterov.Payment.DeletePayment;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Storage.Extension;
using Masterov.Storage.Storages.Masterov.Customer;
using Masterov.Storage.Storages.Masterov.FinishedProduct;
using Masterov.Storage.Storages.Masterov.Payment;
using Masterov.Storage.Storages.Masterov.ProductionOrder;
using Masterov.Storage.Storages.Masterov.ProductType;
using Masterov.Storage.Storages.UserFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Storage.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, string? dbConnectionString)
    {
        // FinishedProduct
        services
            .AddScoped<IGetFinishedProductsStorage, GetFinishedProductsStorage>()
            .AddScoped<IGetFinishedProductByNameStorage, GetFinishedProductByNameStorage>()
            .AddScoped<IAddFinishedProductStorage, AddFinishedProductStorage>()
            .AddScoped<IGetFinishedProductByIdStorage, GetFinishedProductByIdStorage>()
            .AddScoped<IDeleteFinishedProductStorage, DeleteFinishedProductStorage>()
            .AddScoped<IUpdateFinishedProductStorage, UpdateFinishedProductStorage>()
            .AddScoped<IGetOrdersByFinishedProductStorage, GetOrdersByFinishedProductStorage>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddDbContextPool<MasterovDbContext>(options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

        //ProductionOrder
        services
            .AddScoped<IGetProductionOrdersStorage, GetProductionOrdersStorage>()
            .AddScoped<IGetProductionOrdersByCreatedAtStorage, GetProductionOrdersByCreatedAtStorage>()
            .AddScoped<IGetProductionOrdersByCompletedAtStorage, GetProductionOrdersByCompletedAtStorage>()
            .AddScoped<IGetProductionOrdersByDescriptionStorage, GetProductionOrdersByDescriptionStorage>()
            .AddScoped<IGetProductionOrdersByStatusStorage, GetProductionOrdersByStatusStorage>()
            .AddScoped<IGetFinishedProductAtOrderStorage, GetFinishedProductAtOrderStorage>()
            .AddScoped<IGetCustomerByOrderIdStorage, GetCustomerByOrderIdStorage>()
            .AddScoped<IAddProductionOrderStorage, AddProductionOrderStorage>()
            .AddScoped<IGetProductComponentByOrderIdStorage, GetProductComponentByOrderIdStorage>()
            .AddScoped<IUpdateProductionOrderStatusStorage, UpdateProductionOrderStatusStorage>()
            .AddScoped<IDeleteProductionOrderStorage, DeleteProductionOrderStorage>()
            .AddScoped<IUpdateProductionOrderStorage, UpdateProductionOrderStorage>()
            .AddScoped<IGetProductionOrderByOrderIdStorage, GetProductionOrderByOrderIdStorage>();
        
        //ProductType
        services
            .AddScoped<IUpdateProductTypeStorage, UpdateProductTypeStorage>()
            .AddScoped<IDeleteProductTypeStorage, DeleteProductTypeStorage>()
            .AddScoped<IAddProductTypeStorage, AddProductTypeStorage>()
            .AddScoped<IGetProductTypeByNameStorage, GetProductTypeByNameStorage>()
            .AddScoped<IGetProductsTypeStorage, GetProductsTypeStorage>()
            .AddScoped<IGetProductTypeByIdStorage, GetProductTypeByIdStorage>();

        //users
        services
            .AddScoped<IRegisterUserStorage, RegisterUserStorage>()
            .AddScoped<IGetUserByLoginStorage, GetUserByLoginStorage>()
            .AddScoped<IGetUserByIdStorage, GetUserByIdStorage>()
            .AddScoped<IDeleteUserByLoginStorage, DeleteUserByLoginStorage>()
            .AddScoped<IDeleteUserByIdStorage, DeleteUserByIdStorage>()
            .AddScoped<IChangeCustomerFromUserStorage, ChangeCustomerFromUserStorage>()
            .AddScoped<IChangePasswordFromUserStorage, ChangePasswordFromUserStorage>()
            .AddScoped<IChangeRoleUserStorage, ChangeRoleUserStorage>()
            .AddScoped<IGetUsersStorage, GetUsersStorage>();
        
        //customer
        services
            .AddScoped<IAddCustomerStorage, AddCustomerStorage>()
            .AddScoped<IGetCustomersStorage, GetCustomersStorage>()
            .AddScoped<IGetCustomerByNameStorage, GetCustomerByNameStorage>()
            .AddScoped<IDeleteCustomerStorage, DeleteCustomerStorage>()
            .AddScoped<IGetCustomerByPhoneStorage, GetCustomerByPhoneStorage>()
            .AddScoped<IGetCustomerByEmailStorage, GetCustomerByEmailStorage>()
            .AddScoped<IGetOrdersByCustomerIdStorage, GetOrdersByCustomerIdStorage>()
            .AddScoped<IUpdateCustomerStorage, UpdateCustomerStorage>()
            .AddScoped<IGetCustomerByIdStorage, GetCustomerByIdStorage>();
        
        // payment
        services
            .AddScoped<IGetPaymentByIdStorage, GetPaymentByIdStorage>()
            .AddScoped<IGetPaymentsByAmountStorage, GetPaymentsByAmountStorage>()
            .AddScoped<IGetCustomerByPaymentIdStorage, GetCustomerByPaymentIdStorage>()
            .AddScoped<IAddPaymentStorage, AddPaymentStorage>()
            .AddScoped<IGetProductionOrderByPaymentIdStorage, GetProductionOrderByPaymentIdStorage>()
            .AddScoped<IGetPaymentsByPaymentDateStorage, GetPaymentsByPaymentDateStorage>()
            .AddScoped<IGetPaymentsByOrderIdStorage, GetPaymentsByOrderIdStorage>()
            .AddScoped<IDeletePaymentStorage, DeletePaymentStorage>()
            .AddScoped<IAddPaymentStorage, AddPaymentStorage>()
            .AddScoped<IUpdatePaymentStorage, UpdatePaymentStorage>()
            .AddScoped<IGetPaymentsByStatusStorage, GetPaymentsByStatusStorage>()
            .AddScoped<IGetPaymentsStorage, GetPaymentsStorage>();
        
        
        services.AddMemoryCache();
        
        services.AddAutoMapper(config => config
            .AddMaps(Assembly.GetAssembly(typeof(MasterovDbContext))));
        
        return services;
    }
}