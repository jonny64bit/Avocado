import { CRUDGrid, GridOrderDirection } from "@assureddt/pact-vue-grid";
import "@assureddt/pact-vue-grid/dist/pact-vue-grid.css";
import "../global";

new CRUDGrid("#accounts-crud", {
    gridOptions: {
        read: "/accounts/read",
        delete: "/accounts/remove",
        pageSize: 10,
        order: {
            columnName: "firstName",
            direction: GridOrderDirection.ascending,
        },
        allowEdit: true,
        allowAdd: true,
        allowDelete: true,
        deleteColumn: "firstName",
        buttonsWidth: 79,
    },
    gridColumns: [
        {
            name: "firstName",
            display: "First Name",
            type: "text",
        },
        {
            name: "lastName",
            display: "Last Name",
            type: "text",
        },
    ],
    editOptions: {
        add: "/accounts/add",
        edit: "/accounts/edit",
        grabData: "/accounts/data",
        editTitle: "Edit Account",
        addTitle: "Add Account",
    },
    editFields: [
        {
            name: "firstName",
            display: "First Name",
            placeholder: "First Name",
            required: true,
            type: "text",
        },
        {
            name: "lastName",
            display: "Last Name",
            placeholder: "Last Name",
            required: true,
            type: "text",
        }
    ],
    pageTitle: "Accounts",
});