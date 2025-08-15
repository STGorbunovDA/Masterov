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
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSupplierByName;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByName;
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
            .AddScoped<IGetOrdersByFinishedProductUseCase, GetOrdersByFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductsUseCase, GetFinishedProductsUseCase>();

        //ProductionOrder
        services
            .AddScoped<IGetProductionOrdersUseCase, GetProductionOrdersUseCase>()
            .AddScoped<IGetProductionOrdersByCreatedAtUseCase, GetProductionOrdersByCreatedAtUseCase>()
            .AddScoped<IGetProductionOrdersByCompletedAtUseCase, GetProductionOrdersByCompletedAtUseCase>()
            .AddScoped<IGetProductionOrdersByDescriptionUseCase, GetProductionOrdersByDescriptionUseCase>()
            .AddScoped<IGetCustomerByOrderIdUseCase, GetCustomerByOrderIdUseCase>()
            .AddScoped<IGetProductionOrdersByStatusUseCase, GetProductionOrdersByStatusUseCase>()
            .AddScoped<IAddProductionOrderUseCase, AddProductionOrderUseCase>()
            .AddScoped<IDeleteProductionOrderUseCase, DeleteProductionOrderUseCase>()
            .AddScoped<IUpdateProductionOrderStatusUseCase, UpdateProductionOrderStatusUseCase>()
            .AddScoped<IGetFinishedProductAtOrderUseCase, GetFinishedProductAtOrderUseCase>()
            .AddScoped<IUpdateProductionOrderUseCase, UpdateProductionOrderUseCase>()
            .AddScoped<IGetProductComponentByOrderIdUseCase, GetProductComponentByOrderIdUseCase>()
            .AddScoped<IGetProductionOrderByOrdeIdUseCase, GetProductionOrderByOrdeIdUseCase>();
        
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
            .AddScoped<IChangePasswordFromUserUseCase, ChangePasswordFromUserUseCase>()
            .AddScoped<IChangeCustomerFromUserUseCase, ChangeCustomerFromUserUseCase>()
            .AddScoped<IChangeRoleUserByLoginUseCase, ChangeRoleUserByLoginUseCase>()
            .AddScoped<IChangeRoleUserByIdUseCase, ChangeRoleUserByIdUseCase>()
            .AddScoped<IGetUsersUseCase, GetUsersUseCase>();
        
        //customer
        services
            .AddScoped<IAddCustomerUseCase, AddCustomerUseCase>()
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()
            .AddScoped<IGetCustomerByNameUseCase, GetCustomerByNameUseCase>()
            .AddScoped<IGetCustomerByPhoneUseCase, GetCustomerByPhoneUseCase>()
            .AddScoped<IGetOrdersByCustomerIdUseCase, GetOrdersByCustomerIdUseCase>()
            .AddScoped<IGetCustomerByEmailUseCase, GetCustomerByEmailUseCase>()
            .AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>()
            .AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>()
            .AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
        
        //payment
        services
            .AddScoped<IGetPaymentByIdUseCase, GetPaymentByIdUseCase>()
            .AddScoped<IGetPaymentsByAmountUseCase, GetPaymentsByAmountUseCase>()
            .AddScoped<IGetCustomerByPaymentIdUseCase, GetCustomerByPaymentIdUseCase>()
            .AddScoped<IGetProductionOrderByPaymentIdUseCase, GetProductionOrderByPaymentIdUseCase>()
            .AddScoped<IUpdatePaymentUseCase, UpdatePaymentUseCase>()
            .AddScoped<IGetPaymentsByPaymentDateUseCase, GetPaymentsByPaymentDateUseCase>()
            .AddScoped<IDeletePaymentUseCase, DeletePaymentUseCase>()
            .AddScoped<IAddPaymentUseCase, AddPaymentUseCase>()
            .AddScoped<IGetPaymentsByStatusUseCase, GetPaymentsByStatusUseCase>()
            .AddScoped<IGetPaymentsByOrderIdUseCase, GetPaymentsByOrderIdUseCase>()
            .AddScoped<IGetPaymentsUseCase, GetPaymentsUseCase>();
        
        //Supplier
        services
            .AddScoped<IGetSuppliersUseCase, GetSuppliersUseCase>()
            .AddScoped<IGetSupplierByNameUseCase, GetSupplierByNameUseCase>()
            .AddScoped<IGetSupplierByPhoneUseCase, GetSupplierByPhoneUseCase>()
            .AddScoped<IGetSupplierByAddressUseCase, GetSupplierByAddressUseCase>()
            .AddScoped<IGetNewSuppliesBySupplierIdUseCase, GetNewSuppliesBySupplierIdUseCase>()
            .AddScoped<IGetSupplierByIdUseCase, GetSupplierByIdUseCase>();
        
        //Supply
        services
            .AddScoped<IGetSupplyByIdUseCase, GetSupplyByIdUseCase>();
            
        return services;
    }
}