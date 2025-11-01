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
using Masterov.Domain.Masterov.JwtService;
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
using Masterov.Domain.Masterov.ServiceAdditional.ServicePayment;
using Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;
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
using Masterov.Domain.Masterov.UserFolder.LoginUser;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Masterov.UserFolder.UpdateUser;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Domain.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        // FinishedProduct
        services
            .AddScoped<Masterov.FinishedProduct.GetFinishedProductById.IGetFinishedProductByIdUseCase, GetFinishedProductByIdUseCase>()
            .AddScoped<IGetFinishedProductsByNameUseCase, GetFinishedProductsByNameUseCase>()
            .AddScoped<IDeleteFinishedProductUseCase, DeleteFinishedProductUseCase>()
            .AddScoped<IAddFinishedProductUseCase, AddFinishedProductUseCase>()
            .AddScoped<IUpdateFinishedProductUseCase, UpdateFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductsByCreatedAtUseCase, GetFinishedProductsByCreatedAtUseCase>()
            .AddScoped<IGetFinishedProductsWithoutOrdersUseCase, GetFinishedProductsWithoutOrdersUseCase>()
            .AddScoped<IGetFinishedProductByIdWithoutOrdersUseCase, GetFinishedProductByIdWithoutOrdersUseCase>()
            .AddScoped<IGetFinishedProductsByNameWithoutOrdersUseCase, GetFinishedProductsByNameWithoutOrdersUseCase>()
            .AddScoped<IGetFinishedProductsByCreatedAtWithoutOrdersUseCase, GetFinishedProductsByCreatedAtWithoutOrdersUseCase>()
            .AddScoped<IGetFinishedProductsByUpdatedAtWithoutOrdersUseCase, GetFinishedProductsByUpdatedAtWithoutOrdersUseCase>()
            .AddScoped<IGetFinishedProductsByUpdatedAtUseCase, GetFinishedProductsByUpdatedAtUseCase>()
            .AddScoped<IGetOrdersByFinishedProductUseCase, GetOrdersByFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductsUseCase, GetFinishedProductsUseCase>();

        // Order
        services
            .AddScoped<IGetOrdersUseCase, GetOrdersUseCase>()
            .AddScoped<IGetOrdersByCreatedAtUseCase, GetOrdersByCreatedAtUseCase>()
            .AddScoped<IGetOrdersByCompletedAtUseCase, GetOrdersByCompletedAtUseCase>()
            .AddScoped<IGetOrdersByDescriptionUseCase, GetOrdersByDescriptionUseCase>()
            .AddScoped<IGetCustomerByOrderIdUseCase, GetCustomerByOrderIdUseCase>()
            .AddScoped<IGetOrdersByStatusUseCase, GetOrdersByStatusUseCase>()
            .AddScoped<IAddOrderUseCase, AddOrderUseCase>()
            .AddScoped<IGetOrdersByUpdatedAtUseCase, GetOrdersByUpdatedAtUseCase>()
            .AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>()
            .AddScoped<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>()
            .AddScoped<IGetFinishedProductByOrderIdUseCase, GetFinishedProductByOrderIdUseCase>()
            .AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>()
            .AddScoped<IGetUsedComponentsByOrderIdUseCase, UsedUsedComponentsByOrderIdUseCase>()
            .AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        
        // ComponentType
        services
            .AddScoped<IUpdateComponentTypeUseCase, UpdateComponentTypeUseCase>()
            .AddScoped<IDeleteComponentTypeUseCase, DeleteComponentTypeUseCase>()
            .AddScoped<IGetComponentTypesByCreatedAtUseCase, GetComponentTypesByCreatedAtUseCase>()
            .AddScoped<IGetComponentTypesByUpdatedAtUseCase, GetComponentTypesByUpdatedAtUseCase>()
            .AddScoped<IGetComponentTypesByDescriptionUseCase, GetComponentTypesByDescriptionUseCase>()
            .AddScoped<IGetUsedComponentsByComponentTypeIdUseCase, GetUsedComponentsByComponentTypeIdUseCase>()
            .AddScoped<IGetSuppliesByComponentTypeIdUseCase, GetSuppliesByComponentTypeIdUseCase>()
            .AddScoped<IGetComponentTypesByNameUseCase, GetComponentTypesByNameUseCase>()
            .AddScoped<IAddComponentTypeUseCase, AddComponentTypeUseCase>()
            .AddScoped<IGetComponentTypeByIdUseCase, GetComponentTypeByIdUseCase>()
            .AddScoped<IGetComponentTypesUseCase, ComponentTypesUseCase>();
        
        // User
        services
            .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
            .AddScoped<ILoginUserUseCase, LoginUserUseCase>()
            .AddScoped<IGetUserByLoginUseCase, GetUserByLoginUseCase>()
            .AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>()
            .AddScoped<IDeleteUserByLoginUseCase, DeleteUserByLoginUseCase>()
            .AddScoped<IDeleteUserByIdUseCase, DeleteUserByIdUseCase>()
            .AddScoped<IChangePasswordFromUserUseCase, ChangePasswordFromUserUseCase>()
            .AddScoped<IGetUsersByUpdatedAtUseCase, GetUsersByUpdatedAtUseCase>()
            .AddScoped<IChangeAccountLoginDateUserByIdUseCase, ChangeAccountLoginDateUserByIdUseCase>()
            .AddScoped<IChangeCustomerFromUserUseCase, ChangeCustomerFromUserUseCase>()
            .AddScoped<IGetUsersByRoleUseCase, GetUsersByRoleUseCase>()
            .AddScoped<IGetUsersByAccountLoginDateUseCase, GetUsersByAccountLoginDateUseCase>()
            .AddScoped<IGetUsersByCreatedAtUseCase, GetUsersByCreatedAtUseCase>()
            .AddScoped<IUpdateUserUseCase, UpdateUserUseCase>()
            .AddScoped<IChangeRoleUserByLoginUseCase, ChangeRoleUserByLoginUseCase>()
            .AddScoped<IChangeRoleUserByIdUseCase, ChangeRoleUserByIdUseCase>()
            .AddScoped<IGetUsersUseCase, GetUsersUseCase>()
            .AddScoped<IJwtService, JwtService>();;
        
        // Customer
        services
            .AddScoped<IAddCustomerUseCase, AddCustomerUseCase>()
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()
            .AddScoped<IGetCustomersUseCase, GetCustomersUseCase>()
            .AddScoped<IGetCustomersByNameUseCase, GetCustomersByNameUseCase>()
            .AddScoped<IGetCustomerByPhoneUseCase, GetCustomerByPhoneUseCase>()
            .AddScoped<IGetOrdersByCustomerIdUseCase, GetOrdersByCustomerIdUseCase>()
            .AddScoped<IGetCustomersByCreatedAtUseCase, GetCustomersByCreatedAtUseCase>()
            .AddScoped<IGetCustomersByUpdatedAtUseCase, GetCustomersByUpdatedAtUseCase>()
            .AddScoped<IGetCustomerByEmailUseCase, GetCustomerByEmailUseCase>()
            .AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>()
            .AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>()
            .AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
        
        // Payment
        services
            .AddScoped<IGetPaymentByIdUseCase, GetPaymentByIdUseCase>()
            .AddScoped<IGetPaymentsByAmountUseCase, GetPaymentsByAmountUseCase>()
            .AddScoped<IGetCustomerByPaymentIdUseCase, GetCustomerByPaymentIdUseCase>()
            .AddScoped<IGetOrderByPaymentIdUseCase, GetOrderByPaymentIdUseCase>()
            .AddScoped<IUpdatePaymentUseCase, UpdatePaymentUseCase>()
            .AddScoped<IGetPaymentsByCreatedAtUseCase, GetPaymentsByCreatedAtUseCase>()
            .AddScoped<IDeletePaymentUseCase, DeletePaymentUseCase>()
            .AddScoped<IGetPaymentsByUpdatedAtUseCase, GetPaymentsByUpdatedAtUseCase>()
            .AddScoped<IAddPaymentUseCase, AddPaymentUseCase>()
            .AddScoped<IGetPaymentsByStatusUseCase, GetPaymentsByStatusUseCase>()
            .AddScoped<IGetPaymentsByOrderIdUseCase, GetPaymentsByOrderIdUseCase>()
            .AddScoped<IGetPaymentsUseCase, GetPaymentsUseCase>();
        
        // Supplier
        services
            .AddScoped<IGetSuppliersUseCase, GetSuppliersUseCase>()
            .AddScoped<IGetSuppliersByNameUseCase, GetSuppliersByNameUseCase>()
            .AddScoped<IGetSupplierByPhoneUseCase, GetSupplierByPhoneUseCase>()
            .AddScoped<IGetSuppliersByAddressUseCase, GetSuppliersByAddressUseCase>()
            .AddScoped<IGetSupplierByEmailUseCase, GetSupplierByEmailUseCase>()
            .AddScoped<IGetNewSuppliesBySupplierIdUseCase, GetNewSuppliesBySupplierIdUseCase>()
            .AddScoped<IDeleteSupplierUseCase, DeleteSupplierUseCase>()
            .AddScoped<IUpdateSupplierUseCase, UpdateSupplierUseCase>()
            .AddScoped<IGetSuppliersBySurnameUseCase, GetSuppliersBySurnameUseCase>()
            .AddScoped<IAddSupplierUseCase, AddSupplierUseCase>()
            .AddScoped<IGetSupplierByIdUseCase, GetSupplierByIdUseCase>();
        
        // Supply
        services
            .AddScoped<IGetSupplyByIdUseCase, GetSupplyByIdUseCase>()
            .AddScoped<IGetSuppliesByQuantityUseCase, GetSuppliesByQuantityUseCase>()
            .AddScoped<IGetSuppliesByPriceSupplyUseCase, GetSuppliesByPriceSupplyUseCase>()
            .AddScoped<IGetWarehouseBySupplyIdUseCase, GetWarehouseBySupplyIdUseCase>()
            .AddScoped<IGetComponentTypeBySupplyIdUseCase, GetComponentTypeBySupplyIdUseCase>()
            .AddScoped<IGetSupplierBySupplyIdUseCase, GetSupplierBySupplyIdUseCase>()
            .AddScoped<IAddSupplyUseCase, AddSupplyUseCase>()
            .AddScoped<IGetSuppliesBySupplyDateUseCase, GetSuppliesBySupplyDateUseCase>()
            .AddScoped<IUpdateSupplyUseCase, UpdateSupplyUseCase>()
            .AddScoped<IDeleteSupplyUseCase, DeleteSupplyUseCase>()
            .AddScoped<IGetSuppliesUseCase, GetSuppliesUseCase>();

        // Warehouse
        services
            .AddScoped<IGetWarehouseByIdUseCase, GetWarehouseByIdUseCase>()
            .AddScoped<IUpdateWarehouseUseCase, UpdateWarehouseUseCase>()
            .AddScoped<IGetWarehouseByNameUseCase, GetWarehouseByNameUseCase>()
            .AddScoped<IUpdatePriceWarehouseByIdUseCase, UpdatePriceWarehouseByIdUseCase>()
            .AddScoped<IGetSuppliesByWarehouseIdUseCase, GetSuppliesByWarehouseIdUseCase>()
            .AddScoped<IGetWarehousesUseCase, GetWarehousesUseCase>();
        
        // Used Component
        services
            .AddScoped<IGetUsedComponentsUseCase, GetUsedComponentsUseCase>()
            .AddScoped<IGetUsedComponentsByQuantityUseCase, GetUsedComponentsByQuantityUseCase>()
            .AddScoped<IGetUsedComponentsByCreatedAtUseCase, GetUsedComponentsByCreatedAtUseCase>()
            .AddScoped<IGetComponentTypeByUsedComponentIdUseCase, GetComponentTypeByUsedComponentIdUseCase>()
            .AddScoped<IUpdateUsedComponentUseCase, UpdateUsedComponentUseCase>()
            .AddScoped<IGetWarehouseByUsedComponentIdUseCase, GetWarehouseByUsedComponentIdUseCase>()
            .AddScoped<IAddUsedComponentUseCase, AddUsedComponentUseCase>()
            .AddScoped<IUpdateQuantityWarehouseByIdUseCase, UpdateQuantityWarehouseByIdUseCase>()
            .AddScoped<IGetOrderByUsedComponentIdUseCase, GetOrderByUsedComponentIdUseCase>()
            .AddScoped<IDeleteUsedComponentUseCase, DeleteUsedComponentUseCase>()
            .AddScoped<IGetUsedComponentsByUpdatedAtUseCase, GetUsedComponentsByUpdatedAtUseCase>()
            .AddScoped<IGetUsedComponentByIdUseCase, GetUsedComponentByIdUseCase>();

        services
            .AddScoped<IUpdateOrderStatusAfterPayment, UpdateOrderStatusAfterPayment>()
            .AddScoped<IUpdateWarehouseComponentQuantityPrice, UpdateWarehouseComponentQuantityPrice>();
            
        return services;
    }
}