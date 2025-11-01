using System.Reflection;
using Masterov.Domain.Masterov.ComponentType.AddComponentType;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypes;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;
using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;
using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;
using Masterov.Domain.Masterov.ComponentType.UpdateComponentType;
using Masterov.Domain.Masterov.Customer.AddCustomer;
using Masterov.Domain.Masterov.Customer.DeleteCustomer;
using Masterov.Domain.Masterov.Customer.GetCustomerByEmail;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;
using Masterov.Domain.Masterov.Customer.GetCustomersByName;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Masterov.Customer.UpdateCustomer;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.Order.AddOrder;
using Masterov.Domain.Masterov.Order.DeleteOrder;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Masterov.Order.GetOrders;
using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;
using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;
using Masterov.Domain.Masterov.Order.GetOrdersByDescription;
using Masterov.Domain.Masterov.Order.GetOrdersByStatus;
using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;
using Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId;
using Masterov.Domain.Masterov.Order.UpdateOrder;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Masterov.Payment.AddPayment;
using Masterov.Domain.Masterov.Payment.DeletePayment;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;
using Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Masterov.Supplier.AddSupplier;
using Masterov.Domain.Masterov.Supplier.DeleteSupplier;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;
using Masterov.Domain.Masterov.Supplier.GetSuppliers;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;
using Masterov.Domain.Masterov.Supplier.UpdateSupplier;
using Masterov.Domain.Masterov.Supply.AddSupply;
using Masterov.Domain.Masterov.Supply.DeleteSupply;
using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;
using Masterov.Domain.Masterov.Supply.GetSupplies;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;
using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;
using Masterov.Domain.Masterov.Supply.UpdateSupply;
using Masterov.Domain.Masterov.UsedComponent.AddUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;
using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;
using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;
using Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;
using Masterov.Domain.Masterov.UserFolder.GetUsersByRole;
using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Masterov.UserFolder.UpdateUser;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;
using Masterov.Storage.Extension;
using Masterov.Storage.Storages.Masterov.ComponentType;
using Masterov.Storage.Storages.Masterov.Customer;
using Masterov.Storage.Storages.Masterov.FinishedProduct;
using Masterov.Storage.Storages.Masterov.Order;
using Masterov.Storage.Storages.Masterov.Payment;
using Masterov.Storage.Storages.Masterov.Supplier;
using Masterov.Storage.Storages.Masterov.Supply;
using Masterov.Storage.Storages.Masterov.UsedComponent;
using Masterov.Storage.Storages.Masterov.Warehouse;
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
            .AddScoped<IGetFinishedProductByNameStorage, GetFinishedProductsByNameStorage>()
            .AddScoped<IAddFinishedProductStorage, AddFinishedProductStorage>()
            .AddScoped<IGetFinishedProductByIdStorage, GetFinishedProductByIdStorage>()
            .AddScoped<IDeleteFinishedProductStorage, DeleteFinishedProductStorage>()
            .AddScoped<IUpdateFinishedProductStorage, UpdateFinishedProductStorage>()
            .AddScoped<IGetFinishedProductsByCreatedAtStorage, GetFinishedProductsByCreatedAtStorage>()
            .AddScoped<IGetFinishedProductsByUpdatedAtStorage, GetFinishedProductsByUpdatedAtStorage>()
            .AddScoped<IGetFinishedProductsWithoutOrdersStorage, GetFinishedProductsWithoutOrdersStorage>()
            .AddScoped<IGetOrdersByFinishedProductStorage, GetOrdersByFinishedProductStorage>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddDbContextPool<MasterovDbContext>(options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

        // Order
        services
            .AddScoped<IGetOrdersStorage, GetOrdersStorage>()
            .AddScoped<IGetOrdersByCreatedAtStorage, GetOrdersByCreatedAtStorage>()
            .AddScoped<IGetOrdersByCompletedAtStorage, GetOrdersByCompletedAtStorage>()
            .AddScoped<IGetOrdersByDescriptionStorage, GetOrdersByDescriptionStorage>()
            .AddScoped<IGetOrdersByStatusStorage, GetOrdersByStatusStorage>()
            .AddScoped<IGetFinishedProductByOrderIdStorage, GetFinishedProductByOrderIdStorage>()
            .AddScoped<IGetCustomerByOrderIdStorage, GetCustomerByOrderIdStorage>()
            .AddScoped<IAddOrderStorage, AddOrderStorage>()
            .AddScoped<IGetUsedComponentsByOrderIdStorage, GetUsedComponentsByOrderIdStorage>()
            .AddScoped<IGetFinishedProductByIdWithoutOrdersStorage, GetFinishedProductByIdWithoutOrdersStorage>()
            .AddScoped<IGetFinishedProductsByNameWithoutOrdersStorage, GetFinishedProductsByNameWithoutOrdersStorage>()
            .AddScoped<IGetFinishedProductsByCreatedAtWithoutOrdersStorage, GetFinishedProductsByCreatedAtWithoutOrdersStorage>()
            .AddScoped<IGetFinishedProductsByUpdatedAtWithoutOrdersStorage, GetFinishedProductsByUpdatedAtWithoutOrdersStorage>()
            .AddScoped<IGetOrdersByUpdatedAtStorage, GetOrdersByUpdatedAtStorage>()
            .AddScoped<IUpdateOrderStatusStorage, UpdateOrderStatusStorage>()
            .AddScoped<IDeleteOrderStorage, DeleteOrderStorage>()
            .AddScoped<IUpdateOrderStorage, UpdateOrderStorage>()
            .AddScoped<IGetOrderByIdStorage, GetOrderByIdStorage>();
        
        // ComponentType
        services
            .AddScoped<IUpdateComponentTypeStorage, UpdateComponentTypeStorage>()
            .AddScoped<IDeleteComponentTypeStorage, DeleteComponentTypeStorage>()
            .AddScoped<IGetComponentTypesByUpdatedAtStorage, GetComponentTypesByUpdatedAtStorage>()
            .AddScoped<IGetComponentTypesByCreatedAtStorage, GetComponentTypesByCreatedAtStorage>()
            .AddScoped<IAddComponentTypeStorage, AddComponentTypeStorage>()
            .AddScoped<IGetSuppliesByComponentTypeIdStorage, GetSuppliesByComponentTypeIdStorage>()
            .AddScoped<IGetComponentTypesByDescriptionStorage, GetComponentTypesByDescriptionStorage>()
            .AddScoped<IGetComponentTypeByNameStorage, GetComponentTypesByNameStorage>()
            .AddScoped<IGetUsedComponentsByComponentTypeIdStorage, GetUsedComponentsByComponentTypeIdStorage>()
            .AddScoped<IGetComponentTypesStorage, GetComponentTypesStorage>()
            .AddScoped<IGetComponentTypeByIdStorage, GetComponentTypeByIdStorage>();

        // User
        services
            .AddScoped<IRegisterUserStorage, RegisterUserStorage>()
            .AddScoped<IGetUserByLoginStorage, GetUserByLoginStorage>()
            .AddScoped<IGetUserByIdStorage, GetUserByIdStorage>()
            .AddScoped<IDeleteUserByLoginStorage, DeleteUserByLoginStorage>()
            .AddScoped<IDeleteUserByIdStorage, DeleteUserByIdStorage>()
            .AddScoped<IChangeCustomerFromUserStorage, ChangeCustomerFromUserStorage>()
            .AddScoped<IChangePasswordFromUserStorage, ChangePasswordFromUserStorage>()
            .AddScoped<IGetUsersByUpdatedAtStorage, GetUsersByUpdatedAtStorage>()
            .AddScoped<IGetUsersByRoleStorage, GetUsersByRoleStorage>()
            .AddScoped<IChangeAccountLoginDateUserByIdStorage, ChangeAccountLoginDateUserByIdStorage>()
            .AddScoped<IGetUsersByAccountLoginDateStorage, GetUsersByAccountLoginDateStorage>()
            .AddScoped<IGetUsersByCreatedAtStorage, GetUsersByCreatedAtStorage>()
            .AddScoped<IUpdateUserStorage, UpdateUserStorage>()
            .AddScoped<IChangeRoleUserByIdStorage, ChangeRoleUserByIdStorage>()
            .AddScoped<IGetUsersStorage, GetUsersStorage>();
        
        // Customer
        services
            .AddScoped<IAddCustomerStorage, AddCustomerStorage>()
            .AddScoped<IGetCustomersStorage, GetCustomersStorage>()
            .AddScoped<IGetCustomerByNameStorage, GetCustomersByNameStorage>()
            .AddScoped<IDeleteCustomerStorage, DeleteCustomerStorage>()
            .AddScoped<IGetCustomerByPhoneStorage, GetCustomerByPhoneStorage>()
            .AddScoped<IGetCustomerByEmailStorage, GetCustomerByEmailStorage>()
            .AddScoped<IGetOrdersByCustomerIdStorage, GetOrdersByCustomerIdStorage>()
            .AddScoped<IUpdateCustomerStorage, UpdateCustomerStorage>()
            .AddScoped<IGetCustomersByCreatedAtStorage, GetCustomersByCreatedAtStorage>()
            .AddScoped<IGetCustomersByUpdatedAtStorage, GetCustomersByUpdatedAtStorage>()
            .AddScoped<IGetCustomerByIdStorage, GetCustomerByIdStorage>();
        
        // Payment
        services
            .AddScoped<IGetPaymentByIdStorage, GetPaymentByIdStorage>()
            .AddScoped<IGetPaymentsByAmountStorage, GetPaymentsByAmountStorage>()
            .AddScoped<IGetCustomerByPaymentIdStorage, GetCustomerByPaymentIdStorage>()
            .AddScoped<IAddPaymentStorage, AddPaymentStorage>()
            .AddScoped<IGetOrderByPaymentIdStorage, GetOrderByPaymentIdStorage>()
            .AddScoped<IGetPaymentsByCreatedAtStorage, GetPaymentsByCreatedAtStorage>()
            .AddScoped<IGetPaymentsByOrderIdStorage, GetPaymentsByOrderIdStorage>()
            .AddScoped<IGetPaymentsByUpdatedAtStorage, GetPaymentsByUpdatedAtStorage>()
            .AddScoped<IDeletePaymentStorage, DeletePaymentStorage>()
            .AddScoped<IAddPaymentStorage, AddPaymentStorage>()
            .AddScoped<IUpdatePaymentStorage, UpdatePaymentStorage>()
            .AddScoped<IGetPaymentsByStatusStorage, GetPaymentsByStatusStorage>()
            .AddScoped<IGetPaymentsStorage, GetPaymentsStorage>();
        
        // Supplier
        services
            .AddScoped<IGetSuppliersStorage, GetSuppliersStorage>()
            .AddScoped<IGetSupplierByNameStorage, GetSupplierByNameStorage>()
            .AddScoped<IGetSupplierByAddressStorage, GetSupplierByAddressStorage>()
            .AddScoped<IGetSupplierByPhoneStorage, GetSupplierByPhoneStorage>()
            .AddScoped<IGetSupplierByEmailStorage, GetSupplierByEmailStorage>()
            .AddScoped<IGetNewSuppliesBySupplierIdStorage, GetNewSuppliesBySupplierIdStorage>()
            .AddScoped<IGetSuppliersBySurnameStorage, GetSuppliersBySurnameStorage>()
            .AddScoped<IAddSupplierStorage, AddSupplierStorage>()
            .AddScoped<IUpdateSupplierStorage, UpdateSupplierStorage>()
            .AddScoped<IDeleteSupplierStorage, DeleteSupplierStorage>()
            .AddScoped<IGetSupplierByIdStorage, GetSupplierByIdStorage>();
        
        // Supply
        services
            .AddScoped<IGetSupplyByIdStorage, GetSupplyByIdStorage>()
            .AddScoped<IGetSuppliesStorage, GetSuppliesStorage>()
            .AddScoped<IGetSupplierBySupplyIdStorage, GetSupplierBySupplyIdStorage>()
            .AddScoped<IGetSuppliesBySupplyDateStorage, GetSuppliesBySupplyDateStorage>()
            .AddScoped<IGetWarehouseBySupplyIdStorage, GetWarehouseBySupplyIdStorage>()
            .AddScoped<IGetComponentTypeBySupplyIdStorage, GetComponentTypeBySupplyIdStorage>()
            .AddScoped<IAddSupplyStorage, AddSupplyStorage>()
            .AddScoped<IDeleteSupplyStorage, DeleteSupplyStorage>()
            .AddScoped<IGetSuppliesByPriceSupplyStorage, GetSuppliesByPriceSupplyStorage>()
            .AddScoped<IUpdateSupplyStorage, UpdateSupplyStorage>()
            .AddScoped<IGetSuppliesByQuantityStorage, GetSuppliesByQuantityStorage>();
        
        // Warehouse
        services
            .AddScoped<IGetWarehouseByIdStorage, GetWarehouseByIdStorage>()
            .AddScoped<IUpdateWarehouseStorage, UpdateWarehouseStorage>()
            .AddScoped<IGetWarehouseByNameStorage, GetWarehouseByNameStorage>()
            .AddScoped<IUpdatePriceWarehouseByIdStorage, UpdatePriceWarehouseByIdStorage>()
            .AddScoped<IGetSuppliesByWarehouseIdStorage, GetSuppliesByWarehouseIdStorage>()
            .AddScoped<IGetWarehousesStorage, GetWarehousesStorage>();
        
        // Used Component
        services
            .AddScoped<IGetUsedComponentsStorage, GetUsedComponentsStorage>()
            .AddScoped<IGetUsedComponentsByQuantityStorage, GetUsedComponentsByQuantityStorage>()
            .AddScoped<IGetUsedComponentsByCreatedAtStorage, GetUsedComponentsByCreatedAtStorage>()
            .AddScoped<IGetComponentTypeByUsedComponentIdStorage, GetComponentTypeByUsedComponentIdStorage>()
            .AddScoped<IUpdateQuantityWarehouseByIdStorage, UpdateQuantityWarehouseByIdStorage>()
            .AddScoped<IDeleteUsedComponentStorage, DeleteUsedComponentStorage>()
            .AddScoped<IDeleteUsedComponentStorage, DeleteUsedComponentStorage>()
            .AddScoped<IUpdateUsedComponentStorage, UpdateUsedComponentStorage>()
            .AddScoped<IAddUsedComponentStorage, AddUsedComponentStorage>()
            .AddScoped<IGetWarehouseByUsedComponentIdStorage, GetWarehouseByUsedComponentIdStorage>()
            .AddScoped<IGetUsedComponentsByUpdatedAtStorage, GetUsedComponentsByUpdatedAtStorage>()
            .AddScoped<IGetOrderByUsedComponentIdStorage, GetOrderByUsedComponentIdStorage>()
            .AddScoped<IGetUsedComponentByIdStorage, GetUsedComponentByIdStorage>();
        
        services.AddMemoryCache();
        
        services.AddAutoMapper(config => config
            .AddMaps(Assembly.GetAssembly(typeof(MasterovDbContext))));
        
        return services;
    }
}