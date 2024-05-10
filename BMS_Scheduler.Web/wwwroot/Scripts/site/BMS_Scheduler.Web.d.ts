/// <reference path="../../../Modules/_Ext/_q/_q.d.ts" />
/// <reference types="serenity.corelib" />
/// <reference types="serenity.pro.ui" />
/// <reference types="jquery" />
/// <reference types="jqueryui" />
/// <reference types="serenity.extensions" />
/// <reference types="jquery.validation" />
/// <reference types="serenity.pro.extensions" />
declare var isPageRefreshRequired: boolean;
declare namespace q {
    var queryString: {};
    var jsPDFHeaderImageData: string;
    var jsPDFHeaderTitle: string;
    var ListExcelServiceMethodName: string;
    var useSerenityInlineEditors: boolean;
    var DefaultMainGridOptions: ExtGridOptions;
    var DefaultEditorGridOptions: ExtGridOptions;
    var DefaultEntityDialogOptions: ExtDialogOptions;
    var DefaultEditorDialogOptions: ExtDialogOptions;
    var fiscalYearMonths: number[];
}
declare namespace _Ext {
    class GridBase<TItem, TOptions> extends Serenity.EntityGrid<TItem, TOptions> {
        protected getRowType(): {
            idProperty?: string;
            localTextPrefix?: string;
            nameProperty?: string;
            insertPermission?: string;
            updatePermission?: string;
            deletePermission?: string;
        };
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getInsertPermission(): string;
        protected getUpdatePermission(): string;
        protected getDeletePermission(): string;
        protected get_ExtGridOptions(): ExtGridOptions;
        protected isPickerMode(): boolean;
        protected getGrouping(): Slick.GroupInfo<TItem>[];
        isReadOnly: boolean;
        isRequired: boolean;
        isAutosized: boolean;
        isChildGrid: boolean;
        nextRowNumber: number;
        autoColumnSizePlugin: any;
        rowSelection: Serenity.GridRowSelectionMixin;
        pickerDialog: GridItemPickerDialog;
        constructor(container: JQuery, options?: TOptions);
        protected markupReady(): void;
        protected getButtons(): Serenity.ToolButton[];
        protected getReportRequest(): _Ext.ListReportRequest;
        protected getColumns(): Slick.Column[];
        postProcessColumns(columns: Slick.Column[]): Slick.Column[];
        protected createSlickGrid(): Slick.Grid;
        resetColumns(columns: Slick.Column[]): void;
        resizeAllCulumn(): void;
        protected getSlickOptions(): Slick.GridOptions;
        protected getViewOptions(): Slick.RemoteViewOptions;
        protected getPrintRowServiceMethod(): string;
        protected onClick(e: JQueryEventObject, row: number, cell: number): void;
        protected onInlineActionClick(target: JQuery, recordId: any, item: TItem): void;
        protected resetRowNumber(): void;
        setGrouping(groupInfo: Slick.GroupInfo<TItem>[]): void;
        protected getIncludeColumns(include: {
            [key: string]: boolean;
        }): void;
        protected getDefaultSortBy(): any[];
        protected onViewProcessData(response: Serenity.ListResponse<TItem>): Serenity.ListResponse<TItem>;
        initDialog(dialog: DialogBase<TItem, any>): void;
        get selectedItems(): TItem[];
        set selectedKeys(value: any[]);
        protected onViewSubmit(): boolean;
    }
}
declare namespace _Ext {
    class ReportGridBase<TItem, TOptions> extends _Ext.GridBase<TItem, TOptions> {
        protected getButtons(): Serenity.ToolButton[];
        protected getColumns(): Slick.Column[];
    }
}
declare namespace _Ext {
    class ReportPanelBase<TRequest> extends Serenity.PropertyPanel<TRequest, any> {
        protected getReportTitle(): string;
        protected getReportKey(): string;
        protected getReportRequest(): TRequest;
        constructor(container: JQuery, opt?: any);
        protected getTemplate(): string;
    }
}
declare namespace _Ext {
    class DialogBase<TEntity, TOptions> extends Serenity.EntityDialog<TEntity, TOptions> {
        protected getRowType(): {
            idProperty?: string;
            localTextPrefix?: string;
            nameProperty?: string;
            insertPermission?: string;
            updatePermission?: string;
            deletePermission?: string;
        };
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getInsertPermission(): string;
        protected getUpdatePermission(): string;
        protected getDeletePermission(): string;
        protected get_ExtDialogOptions(): ExtDialogOptions;
        protected loadedState: string;
        isReadOnly: boolean;
        protected form: any;
        constructor(opt?: any);
        protected updateInterface(): void;
        protected onDialogOpen(): void;
        protected onDialogClose(): void;
        protected setReadOnly(value: boolean): void;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected onRefreshClick(): void;
        protected onSaveAndNewButtonClick(): void;
        protected getSaveState(): string;
        protected onSaveSuccess(response: any): void;
        loadResponse(data: any): void;
        maximize(): void;
        fullContentArea(): void;
        setDialogSize(width?: any, height?: any, top?: any, left?: any, $content?: any): void;
        onAfterSetDialogSize(): void;
        onAfterDialogClose(entity: TEntity): void;
        parentGrid: GridBase<TEntity, any>;
    }
}
declare namespace _Ext {
    class GridItemPickerDialog extends Serenity.TemplatedDialog<GridItemPickerEditorOptions> {
        getTemplate(): string;
        checkGrid: GridBase<any, GridItemPickerEditorOptions>;
        get selectedItems(): any[];
        constructor(options: GridItemPickerEditorOptions);
        onSuccess: (selectedItems: any) => void;
        getDialogOptions(): JQueryUI.DialogOptions;
    }
}
declare namespace _Ext {
    class EditorDialogBase<TEntity> extends DialogBase<TEntity, any> {
        protected get_ExtDialogOptions(): ExtDialogOptions;
        protected getIdProperty(): string;
        onSave: (options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void) => void;
        onDelete: (options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void) => void;
        destroy(): void;
        protected updateInterface(): void;
        protected saveHandler(options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void): void;
        protected deleteHandler(options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void): void;
        parentEditor: GridEditorBase<TEntity>;
    }
}
declare namespace _Ext {
    class GridEditorBaseWithOption<TEntity, TOptions> extends _Ext.GridBase<TEntity, TOptions> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        protected get_ExtGridOptions(): ExtGridOptions;
        protected getIdProperty(): string;
        isChildGrid: boolean;
        protected nextId: number;
        constructor(container: JQuery, options?: any);
        private sortGridFunction;
        protected getQuickFilters(): any[];
        protected id(entity: TEntity): any;
        protected save(opt: Serenity.ServiceOptions<any>, callback: (r: Serenity.ServiceResponse) => void): void;
        protected deleteEntity(id: number): boolean;
        protected validateEntity(row: TEntity, id: number): boolean;
        protected getNewEntity(): TEntity;
        protected getButtons(): Serenity.ToolButton[];
        protected addButtonClick(): void;
        protected editItem(entityOrId: any): void;
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        get value(): TEntity[];
        set value(value: TEntity[]);
        protected getGridCanLoad(): boolean;
        protected usePager(): boolean;
        protected getInitialTitle(): any;
        private searchText;
        protected createToolbarExtensions(): void;
        protected onViewFilter(row: any): boolean;
        private matchContains;
        getFilteredItems(): TEntity[];
        protected enableFiltering(): boolean;
        protected onViewSubmit(): boolean;
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        protected getSlickOptions(): Slick.GridOptions;
        parentDialog: DialogBase<any, any>;
        onItemsChanged(): void;
        onBeforeGetValue(items: TEntity[]): void;
    }
    class GridEditorBase<TEntity> extends GridEditorBaseWithOption<TEntity, any> {
    }
    class GridEditorBaseForJsonField<TEntity> extends GridEditorBaseWithOption<TEntity, any> {
        protected getRowIdField(): string;
        getEditValue(property: any, target: any): void;
    }
}
declare namespace _Ext {
    class JsonGridEditorBase<TEntity> extends _Ext.GridEditorBase<TEntity> {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
    }
}
declare namespace BMS_Scheduler.Administration {
    class LanguageColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface LanguageForm {
        LanguageId: Serenity.StringEditor;
        LanguageName: Serenity.StringEditor;
    }
    class LanguageForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Administration {
    interface LanguageRow {
        Id?: number;
        LanguageId?: string;
        LanguageName?: string;
    }
    namespace LanguageRow {
        const idProperty = "Id";
        const nameProperty = "LanguageName";
        const localTextPrefix = "Administration.Language";
        const lookupKey = "Administration.Language";
        function getLookup(): Q.Lookup<LanguageRow>;
        const deletePermission = "Administration:Translation";
        const insertPermission = "Administration:Translation";
        const readPermission = "Administration:Translation";
        const updatePermission = "Administration:Translation";
        const enum Fields {
            Id = "Id",
            LanguageId = "LanguageId",
            LanguageName = "LanguageName"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace LanguageService {
        const baseUrl = "Administration/Language";
        function Create(request: Serenity.SaveRequest<LanguageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<LanguageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<LanguageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<LanguageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "Administration/Language/Create",
            Update = "Administration/Language/Update",
            Delete = "Administration/Language/Delete",
            Retrieve = "Administration/Language/Retrieve",
            List = "Administration/Language/List"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace PermissionKeys {
        const Security = "Administration:Security";
        const Translation = "Administration:Translation";
    }
}
declare namespace BMS_Scheduler.Administration {
    class RoleColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RoleForm {
        RoleName: Serenity.StringEditor;
    }
    class RoleForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RolePermissionListRequest extends Serenity.ServiceRequest {
        RoleID?: number;
        Module?: string;
        Submodule?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RolePermissionListResponse extends Serenity.ListResponse<string> {
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RolePermissionRow {
        RolePermissionId?: number;
        RoleId?: number;
        PermissionKey?: string;
        RoleRoleName?: string;
    }
    namespace RolePermissionRow {
        const idProperty = "RolePermissionId";
        const nameProperty = "PermissionKey";
        const localTextPrefix = "Administration.RolePermission";
        const deletePermission = "Administration:Security";
        const insertPermission = "Administration:Security";
        const readPermission = "Administration:Security";
        const updatePermission = "Administration:Security";
        const enum Fields {
            RolePermissionId = "RolePermissionId",
            RoleId = "RoleId",
            PermissionKey = "PermissionKey",
            RoleRoleName = "RoleRoleName"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace RolePermissionService {
        const baseUrl = "Administration/RolePermission";
        function Update(request: RolePermissionUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: RolePermissionListRequest, onSuccess?: (response: RolePermissionListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Update = "Administration/RolePermission/Update",
            List = "Administration/RolePermission/List"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RolePermissionUpdateRequest extends Serenity.ServiceRequest {
        RoleID?: number;
        Module?: string;
        Submodule?: string;
        Permissions?: string[];
    }
}
declare namespace BMS_Scheduler.Administration {
    interface RoleRow {
        RoleId?: number;
        RoleName?: string;
    }
    namespace RoleRow {
        const idProperty = "RoleId";
        const nameProperty = "RoleName";
        const localTextPrefix = "Administration.Role";
        const lookupKey = "Administration.Role";
        function getLookup(): Q.Lookup<RoleRow>;
        const deletePermission = "Administration:Role:Delete";
        const insertPermission = "Administration:Role:Insert";
        const readPermission = "Administration:Role:Read";
        const updatePermission = "Administration:Role:Update";
        const enum Fields {
            RoleId = "RoleId",
            RoleName = "RoleName"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace RoleService {
        const baseUrl = "Administration/Role";
        function Create(request: Serenity.SaveRequest<RoleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<RoleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RoleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RoleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "Administration/Role/Create",
            Update = "Administration/Role/Update",
            Delete = "Administration/Role/Delete",
            Retrieve = "Administration/Role/Retrieve",
            List = "Administration/Role/List"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    interface TranslationItem {
        Key?: string;
        SourceText?: string;
        TargetText?: string;
        CustomText?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface TranslationListRequest extends Serenity.ListRequest {
        SourceLanguageID?: string;
        TargetLanguageID?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace TranslationService {
        const baseUrl = "Administration/Translation";
        function List(request: TranslationListRequest, onSuccess?: (response: Serenity.ListResponse<TranslationItem>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: TranslationUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            List = "Administration/Translation/List",
            Update = "Administration/Translation/Update"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    interface TranslationUpdateRequest extends Serenity.ServiceRequest {
        TargetLanguageID?: string;
        Translations?: {
            [key: string]: string;
        };
    }
}
declare namespace BMS_Scheduler.Administration {
    class UserColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserForm {
        Username: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        PasswordConfirm: Serenity.PasswordEditor;
        Source: Serenity.StringEditor;
        UserImage: Serenity.ImageUploadEditor;
    }
    class UserForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserPermissionListRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Module?: string;
        Submodule?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserPermissionRow {
        UserPermissionId?: number;
        UserId?: number;
        PermissionKey?: string;
        Granted?: boolean;
        Username?: string;
        User?: string;
    }
    namespace UserPermissionRow {
        const idProperty = "UserPermissionId";
        const nameProperty = "PermissionKey";
        const localTextPrefix = "Administration.UserPermission";
        const deletePermission = "Administration:Security";
        const insertPermission = "Administration:Security";
        const readPermission = "Administration:Security";
        const updatePermission = "Administration:Security";
        const enum Fields {
            UserPermissionId = "UserPermissionId",
            UserId = "UserId",
            PermissionKey = "PermissionKey",
            Granted = "Granted",
            Username = "Username",
            User = "User"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace UserPermissionService {
        const baseUrl = "Administration/UserPermission";
        function Update(request: UserPermissionUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: UserPermissionListRequest, onSuccess?: (response: Serenity.ListResponse<UserPermissionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ListRolePermissions(request: UserPermissionListRequest, onSuccess?: (response: Serenity.ListResponse<string>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ListPermissionKeys(request: Serenity.ServiceRequest, onSuccess?: (response: Serenity.ListResponse<string>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Update = "Administration/UserPermission/Update",
            List = "Administration/UserPermission/List",
            ListRolePermissions = "Administration/UserPermission/ListRolePermissions",
            ListPermissionKeys = "Administration/UserPermission/ListPermissionKeys"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserPermissionUpdateRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Module?: string;
        Submodule?: string;
        Permissions?: UserPermissionRow[];
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserRoleListRequest extends Serenity.ServiceRequest {
        UserID?: number;
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserRoleListResponse extends Serenity.ListResponse<number> {
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserRoleRow {
        UserRoleId?: number;
        UserId?: number;
        RoleId?: number;
        Username?: string;
        User?: string;
    }
    namespace UserRoleRow {
        const idProperty = "UserRoleId";
        const localTextPrefix = "Administration.UserRole";
        const deletePermission = "Administration:Security";
        const insertPermission = "Administration:Security";
        const readPermission = "?";
        const updatePermission = "Administration:Security";
        const enum Fields {
            UserRoleId = "UserRoleId",
            UserId = "UserId",
            RoleId = "RoleId",
            Username = "Username",
            User = "User"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace UserRoleService {
        const baseUrl = "Administration/UserRole";
        function Update(request: UserRoleUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: UserRoleListRequest, onSuccess?: (response: UserRoleListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Update = "Administration/UserRole/Update",
            List = "Administration/UserRole/List"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserRoleUpdateRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Roles?: number[];
    }
}
declare namespace BMS_Scheduler.Administration {
    interface UserRow {
        UserId?: number;
        Username?: string;
        Source?: string;
        PasswordHash?: string;
        PasswordSalt?: string;
        DisplayName?: string;
        Email?: string;
        UserImage?: string;
        LastDirectoryUpdate?: string;
        IsActive?: number;
        Password?: string;
        PasswordConfirm?: string;
        IsAdmin?: boolean;
        InsertUserId?: number;
        InsertDate?: string;
        UpdateUserId?: number;
        UpdateDate?: string;
    }
    namespace UserRow {
        const idProperty = "UserId";
        const isActiveProperty = "IsActive";
        const nameProperty = "DisplayName";
        const localTextPrefix = "Administration.User";
        const lookupKey = "Administration.User";
        function getLookup(): Q.Lookup<UserRow>;
        const deletePermission = "Administration:User:Delete";
        const insertPermission = "Administration:User:Insert";
        const readPermission = "?";
        const updatePermission = "Administration:User:Update";
        const enum Fields {
            UserId = "UserId",
            Username = "Username",
            Source = "Source",
            PasswordHash = "PasswordHash",
            PasswordSalt = "PasswordSalt",
            DisplayName = "DisplayName",
            Email = "Email",
            UserImage = "UserImage",
            LastDirectoryUpdate = "LastDirectoryUpdate",
            IsActive = "IsActive",
            Password = "Password",
            PasswordConfirm = "PasswordConfirm",
            IsAdmin = "IsAdmin",
            InsertUserId = "InsertUserId",
            InsertDate = "InsertDate",
            UpdateUserId = "UpdateUserId",
            UpdateDate = "UpdateDate"
        }
    }
}
declare namespace BMS_Scheduler.Administration {
    namespace UserService {
        const baseUrl = "Administration/User";
        function Create(request: Serenity.SaveRequest<UserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<UserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Undelete(request: Serenity.UndeleteRequest, onSuccess?: (response: Serenity.UndeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<UserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<UserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "Administration/User/Create",
            Update = "Administration/User/Update",
            Delete = "Administration/User/Delete",
            Undelete = "Administration/User/Undelete",
            Retrieve = "Administration/User/Retrieve",
            List = "Administration/User/List"
        }
    }
}
declare namespace BMS_Scheduler {
    interface ApprovalFlowRequest extends Serenity.ServiceRequest {
        approvalProcess?: BMS_Scheduler.Common.ApprovalProcess;
        ApprovalFlowDetailId?: number;
        ApprovalStatusId?: number;
        ApplicationId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface ApprovalFlowResponse extends Serenity.ServiceResponse {
        IsSuccess?: boolean;
        Message?: string;
    }
}
declare namespace BMS_Scheduler {
    interface ApprovalFlowScopeRequest extends Serenity.ServiceRequest {
        approvalProcess?: BMS_Scheduler.Common.ApprovalProcess;
    }
}
declare namespace BMS_Scheduler {
    interface ApprovalFlowScopeResponse extends Serenity.ServiceResponse {
        IsRequired?: boolean;
    }
}
declare namespace BMS_Scheduler {
    interface ApproverRequest extends Serenity.ServiceRequest {
        approvalProcess?: BMS_Scheduler.Common.ApprovalProcess;
    }
}
declare namespace BMS_Scheduler {
    interface ApproverResponse extends Serenity.ServiceResponse {
    }
}
declare namespace BMS_Scheduler {
    interface AutoGeneratedCodeConfigRequest extends Serenity.ServiceRequest {
        ConfigurationFor?: BMS_Scheduler.Common.AutoGeneratedCodeConfiguration;
    }
}
declare namespace BMS_Scheduler {
    interface AutoGeneratedCodeConfigResponse extends Serenity.ServiceResponse {
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoEditorColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CompanyInfoEditorForm {
        Name: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        WebSite: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Fax: Serenity.StringEditor;
        Logo: Serenity.StringEditor;
    }
    class CompanyInfoEditorForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CompanyInfoForm {
        Name: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        WebSite: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Fax: Serenity.StringEditor;
        Logo: Serenity.StringEditor;
    }
    class CompanyInfoForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CompanyInfoRow {
        Id?: number;
        Name?: string;
        Address?: string;
        WebSite?: string;
        Phone?: string;
        Fax?: string;
        Logo?: string;
    }
    namespace CompanyInfoRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "BhasaniTask.CompanyInfo";
        const deletePermission = "BhasaniTask:CompanyInfo:Delete";
        const insertPermission = "BhasaniTask:CompanyInfo:Insert";
        const readPermission = "BhasaniTask:CompanyInfo:Read";
        const updatePermission = "BhasaniTask:CompanyInfo:Update";
        const enum Fields {
            Id = "Id",
            Name = "Name",
            Address = "Address",
            WebSite = "WebSite",
            Phone = "Phone",
            Fax = "Fax",
            Logo = "Logo"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    namespace CompanyInfoService {
        const baseUrl = "BhasaniTask/CompanyInfo";
        function Create(request: Serenity.SaveRequest<CompanyInfoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CompanyInfoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CompanyInfoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CompanyInfoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "BhasaniTask/CompanyInfo/Create",
            Update = "BhasaniTask/CompanyInfo/Update",
            Delete = "BhasaniTask/CompanyInfo/Delete",
            Retrieve = "BhasaniTask/CompanyInfo/Retrieve",
            List = "BhasaniTask/CompanyInfo/List"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryEditorColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CountryEditorForm {
        Name: Serenity.StringEditor;
    }
    class CountryEditorForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CountryForm {
        Name: Serenity.StringEditor;
    }
    class CountryForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface CountryRow {
        Id?: number;
        Name?: string;
    }
    namespace CountryRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "BhasaniTask.Country";
        const lookupKey = "BhasaniTask:Country";
        function getLookup(): Q.Lookup<CountryRow>;
        const deletePermission = "?";
        const insertPermission = "?";
        const readPermission = "?";
        const updatePermission = "?";
        const enum Fields {
            Id = "Id",
            Name = "Name"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    namespace CountryService {
        const baseUrl = "BhasaniTask/Country";
        function Create(request: Serenity.SaveRequest<CountryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CountryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CountryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CountryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "BhasaniTask/Country/Create",
            Update = "BhasaniTask/Country/Update",
            Delete = "BhasaniTask/Country/Delete",
            Retrieve = "BhasaniTask/Country/Retrieve",
            List = "BhasaniTask/Country/List"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingEditorColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingEditorForm {
        OrderNo: Serenity.StringEditor;
        InVoiceNo: Serenity.StringEditor;
        InVoiceDate: Serenity.DateEditor;
        Consignee: Serenity.StringEditor;
        TotalAmount: Serenity.DecimalEditor;
        TotalWeight: Serenity.DecimalEditor;
        VesselOrFlightNo: Serenity.StringEditor;
        PaymentTerms: Serenity.StringEditor;
        PackingSlipNo: Serenity.StringEditor;
        TotalBox: Serenity.IntegerEditor;
        CountryOfOriginOfGoods: Serenity.LookupEditor;
        FinalDestination: Serenity.LookupEditor;
        Measure: Serenity.StringEditor;
    }
    class ShippingEditorForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingForm {
        OrderNo: Serenity.StringEditor;
        InVoiceNo: Serenity.StringEditor;
        InVoiceDate: Serenity.DateEditor;
        Consignee: Serenity.StringEditor;
        VesselOrFlightNo: Serenity.StringEditor;
        PaymentTerms: Serenity.StringEditor;
        PackingSlipNo: Serenity.StringEditor;
        CountryOfOriginOfGoods: Serenity.LookupEditor;
        FinalDestination: Serenity.LookupEditor;
        ShippingItemList: ShippingItemsGridEditor;
        TotalAmount: Serenity.DecimalEditor;
        TotalWeight: Serenity.DecimalEditor;
        TotalBox: Serenity.IntegerEditor;
        Measure: Serenity.StringEditor;
    }
    class ShippingForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsEditorColumns {
        static columnsKey: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingItemsEditorForm {
        ShippingId: Serenity.IntegerEditor;
        DescriptionOfGoods: Serenity.StringEditor;
        Traiff: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        Uom: Serenity.StringEditor;
        UnitRate: Serenity.DecimalEditor;
        TotalRate: Serenity.DecimalEditor;
    }
    class ShippingItemsEditorForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingItemsForm {
        DescriptionOfGoods: Serenity.StringEditor;
        Traiff: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        Uom: Serenity.StringEditor;
        UnitRate: Serenity.DecimalEditor;
        TotalRate: Serenity.DecimalEditor;
    }
    class ShippingItemsForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingItemsRow {
        Id?: number;
        ShippingId?: number;
        DescriptionOfGoods?: string;
        Traiff?: string;
        Quantity?: number;
        Uom?: string;
        UnitRate?: number;
        TotalRate?: number;
        ShippingOrderNo?: string;
        ShippingInVoiceNo?: string;
        ShippingInVoiceDate?: string;
        ShippingConsignee?: string;
        ShippingTotalAmount?: number;
        ShippingTotalWeight?: number;
        ShippingVesselOrFlightNo?: string;
        ShippingPaymentTerms?: string;
        ShippingPackingSlipNo?: string;
        ShippingTotalBox?: number;
        ShippingCountryOfOriginOfGoods?: number;
        ShippingFinalDestination?: number;
        ShippingMeasure?: string;
    }
    namespace ShippingItemsRow {
        const idProperty = "Id";
        const nameProperty = "DescriptionOfGoods";
        const localTextPrefix = "BhasaniTask.ShippingItems";
        const deletePermission = "?";
        const insertPermission = "?";
        const readPermission = "?";
        const updatePermission = "?";
        const enum Fields {
            Id = "Id",
            ShippingId = "ShippingId",
            DescriptionOfGoods = "DescriptionOfGoods",
            Traiff = "Traiff",
            Quantity = "Quantity",
            Uom = "Uom",
            UnitRate = "UnitRate",
            TotalRate = "TotalRate",
            ShippingOrderNo = "ShippingOrderNo",
            ShippingInVoiceNo = "ShippingInVoiceNo",
            ShippingInVoiceDate = "ShippingInVoiceDate",
            ShippingConsignee = "ShippingConsignee",
            ShippingTotalAmount = "ShippingTotalAmount",
            ShippingTotalWeight = "ShippingTotalWeight",
            ShippingVesselOrFlightNo = "ShippingVesselOrFlightNo",
            ShippingPaymentTerms = "ShippingPaymentTerms",
            ShippingPackingSlipNo = "ShippingPackingSlipNo",
            ShippingTotalBox = "ShippingTotalBox",
            ShippingCountryOfOriginOfGoods = "ShippingCountryOfOriginOfGoods",
            ShippingFinalDestination = "ShippingFinalDestination",
            ShippingMeasure = "ShippingMeasure"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    namespace ShippingItemsService {
        const baseUrl = "BhasaniTask/ShippingItems";
        function Create(request: Serenity.SaveRequest<ShippingItemsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ShippingItemsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ShippingItemsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ShippingItemsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "BhasaniTask/ShippingItems/Create",
            Update = "BhasaniTask/ShippingItems/Update",
            Delete = "BhasaniTask/ShippingItems/Delete",
            Retrieve = "BhasaniTask/ShippingItems/Retrieve",
            List = "BhasaniTask/ShippingItems/List"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    interface ShippingRow {
        Id?: number;
        OrderNo?: string;
        InVoiceNo?: string;
        InVoiceDate?: string;
        Consignee?: string;
        TotalAmount?: number;
        TotalWeight?: number;
        VesselOrFlightNo?: string;
        PaymentTerms?: string;
        PackingSlipNo?: string;
        TotalBox?: number;
        CountryOfOriginOfGoods?: number;
        FinalDestination?: number;
        Measure?: string;
        ShippingItemList?: ShippingItemsRow[];
    }
    namespace ShippingRow {
        const idProperty = "Id";
        const nameProperty = "OrderNo";
        const localTextPrefix = "BhasaniTask.Shipping";
        const deletePermission = "BhasaniTask:Shipping:Delete";
        const insertPermission = "BhasaniTask:Shipping:Insert";
        const readPermission = "BhasaniTask:Shipping:Read";
        const updatePermission = "BhasaniTask:Shipping:Update";
        const enum Fields {
            Id = "Id",
            OrderNo = "OrderNo",
            InVoiceNo = "InVoiceNo",
            InVoiceDate = "InVoiceDate",
            Consignee = "Consignee",
            TotalAmount = "TotalAmount",
            TotalWeight = "TotalWeight",
            VesselOrFlightNo = "VesselOrFlightNo",
            PaymentTerms = "PaymentTerms",
            PackingSlipNo = "PackingSlipNo",
            TotalBox = "TotalBox",
            CountryOfOriginOfGoods = "CountryOfOriginOfGoods",
            FinalDestination = "FinalDestination",
            Measure = "Measure",
            ShippingItemList = "ShippingItemList"
        }
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    namespace ShippingService {
        const baseUrl = "BhasaniTask/Shipping";
        function Create(request: Serenity.SaveRequest<ShippingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ShippingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ShippingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ShippingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "BhasaniTask/Shipping/Create",
            Update = "BhasaniTask/Shipping/Update",
            Delete = "BhasaniTask/Shipping/Delete",
            Retrieve = "BhasaniTask/Shipping/Retrieve",
            List = "BhasaniTask/Shipping/List"
        }
    }
}
declare namespace BMS_Scheduler.Common {
    enum ApprovalProcess {
        FPC = 1,
        RO = 2,
        ClientContract = 3,
        Invoice = 4
    }
}
declare namespace BMS_Scheduler.Common {
    enum ApprovalStatus {
        Draft = 1,
        Submit = 2,
        Recommend = 3,
        Approve = 4,
        Reject = 5,
        Cancel = 6,
        ReSubmit = 7,
        Delete = 8
    }
}
declare namespace BMS_Scheduler.Common {
    enum AutoGeneratedCodeConfiguration {
        Invoice = 10,
        TimeCode = 20,
        Client = 30,
        Agency = 40,
        Ro = 50,
        Program = 60,
        ClientContract = 70
    }
}
declare namespace BMS_Scheduler.Common {
    enum IdentityClaimType {
        TvChannelId = 0
    }
}
declare namespace BMS_Scheduler.Common {
    enum ProgramTelecastType {
        Regular = 1,
        Repeat = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum RundownPositionPermission {
        Commercial = 10,
        SegmentBody = 20,
        All = 100
    }
}
declare namespace BMS_Scheduler.Common {
    enum SegmentType {
        ByTimeSlot = 1,
        ByProgramBanner = 2,
        ByProgramCategory = 3,
        ByProgramName = 4
    }
}
declare namespace BMS_Scheduler.Common {
    enum SpotDurationType {
        BySpot = 1,
        BySecond = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum SpotType {
        Paid = 1,
        Bonus = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum TVCPlacementPosition {
        CommercialBreak = 1,
        SegmentBody = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum TVCSpotType {
        Multispot = 1,
        SingleSpot = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum UserType {
        Internal = 1,
        External = 2
    }
}
declare namespace BMS_Scheduler.Common {
    enum WorkflowType {
        FPC = 1,
        RO = 2,
        Rundown = 3,
        TimeSlotSegment = 4
    }
}
declare namespace BMS_Scheduler.Common {
    enum YesNoType {
        No = 0,
        Yes = 1
    }
}
declare namespace BMS_Scheduler {
    interface ContentDatabaseRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        StartTime?: string;
        EndTime?: string;
        ProgramId?: number;
        ProgramCategoryId?: number;
        SpotPositionId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface ContentDatabaseResponse extends Serenity.ServiceResponse {
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownDataRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        TimeSegmentId?: number;
        PositionId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownDataResponse extends Serenity.ServiceResponse {
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownExistanceRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownExistanceResponse extends Serenity.ServiceResponse {
        RundownId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownExportRequest extends Serenity.ServiceRequest {
        Id?: number;
        FileName?: string;
        ExportColumns?: string[];
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownExportResponse extends Serenity.ServiceResponse {
        Rundowns?: DailyRundownInformation[];
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownInformation {
        Category?: string;
        TimeCode?: string;
        Duration?: string;
        StartTime?: string;
        EndTime?: string;
        Remarks?: string;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentCopyRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        RundownTimeSegmentId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentCopyResponse extends Serenity.ServiceResponse {
        IsSuccess?: boolean;
        Message?: string;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentResponse extends Serenity.ServiceResponse {
        Message?: string;
        IsSuccess?: boolean;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentSaveRequest extends Serenity.ServiceRequest {
        TimeSlotDetailId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface DailyRundownTimeSegmentSaveResponse extends Serenity.ServiceResponse {
        IsSuccess?: boolean;
        Message?: string;
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ChangePasswordForm {
        OldPassword: Serenity.PasswordEditor;
        NewPassword: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
    class ChangePasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ChangePasswordRequest extends Serenity.ServiceRequest {
        OldPassword?: string;
        NewPassword?: string;
        ConfirmPassword?: string;
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ForgotPasswordForm {
        Email: Serenity.EmailAddressEditor;
    }
    class ForgotPasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ForgotPasswordRequest extends Serenity.ServiceRequest {
        Email?: string;
    }
}
declare namespace BMS_Scheduler.Membership {
    interface LoginForm {
        Username: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
    }
    class LoginForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Membership {
    interface LoginRequest extends Serenity.ServiceRequest {
        Username?: string;
        Password?: string;
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ResetPasswordForm {
        NewPassword: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
    class ResetPasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Membership {
    interface ResetPasswordRequest extends Serenity.ServiceRequest {
        Token?: string;
        NewPassword?: string;
        ConfirmPassword?: string;
    }
}
declare namespace BMS_Scheduler.Membership {
    interface SignUpForm {
        DisplayName: Serenity.StringEditor;
        Email: Serenity.EmailAddressEditor;
        ConfirmEmail: Serenity.EmailAddressEditor;
        Password: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
    class SignUpForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace BMS_Scheduler.Membership {
    interface SignUpRequest extends Serenity.ServiceRequest {
        DisplayName?: string;
        Email?: string;
        Password?: string;
    }
}
declare namespace BMS_Scheduler {
    interface ScriptUserDefinition {
        Username?: string;
        DisplayName?: string;
        IsAdmin?: boolean;
        Permissions?: {
            [key: string]: boolean;
        };
        TVChannelId?: number;
        UserId?: number;
    }
}
declare namespace BMS_Scheduler {
    interface TelecastTypeRequest extends Serenity.ServiceRequest {
        ProgramId?: number;
        EpisodeNumber?: number;
        SegmentNumber?: number;
        TelecastTime?: string;
    }
}
declare namespace BMS_Scheduler {
    interface TelecastTypeResponse extends Serenity.ServiceResponse {
        TelecastType?: BMS_Scheduler.Common.ProgramTelecastType;
        EpisodeNumber?: number;
    }
}
declare namespace BMS_Scheduler.Texts {
}
declare namespace BMS_Scheduler {
    interface TimeSegmentRequest extends Serenity.ServiceRequest {
        RundownDate?: string;
        FromTime?: number;
        ToTime?: number;
    }
}
declare namespace BMS_Scheduler {
    interface TimeSegmentResponse extends Serenity.ServiceResponse {
        StartTime?: string;
        EndTime?: string;
        IsSuccess?: boolean;
        Message?: string;
    }
}
declare namespace _Ext {
    enum AuditActionType {
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
declare namespace _Ext {
    class AuditLogColumns {
        static columnsKey: string;
    }
}
declare namespace _Ext {
    interface AuditLogForm {
        FeatureName: Serenity.StringEditor;
        EntityTableName: Serenity.StringEditor;
        ActionType: Serenity.EnumEditor;
        FormattedActionDate: Serenity.StringEditor;
        Changes: StaticTextBlock;
        UserId: Serenity.LookupEditor;
        IpAddress: Serenity.StringEditor;
        SessionId: Serenity.StringEditor;
        EntityId: Serenity.IntegerEditor;
    }
    class AuditLogForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace _Ext {
    interface AuditLogRow {
        Id?: number;
        UserId?: number;
        ActionType?: AuditActionType;
        ActionDate?: string;
        FormattedActionDate?: string;
        EntityTableName?: string;
        EntityId?: number;
        Changes?: string;
        IpAddress?: string;
        SessionId?: string;
        FeatureName?: string;
    }
    namespace AuditLogRow {
        const idProperty = "Id";
        const nameProperty = "EntityTableName";
        const localTextPrefix = "_Ext.AuditLog";
        const deletePermission = "Administration:AuditLog";
        const insertPermission = "Administration:AuditLog";
        const readPermission = "Administration:AuditLog";
        const updatePermission = "Administration:AuditLog";
        const enum Fields {
            Id = "Id",
            UserId = "UserId",
            ActionType = "ActionType",
            ActionDate = "ActionDate",
            FormattedActionDate = "FormattedActionDate",
            EntityTableName = "EntityTableName",
            EntityId = "EntityId",
            Changes = "Changes",
            IpAddress = "IpAddress",
            SessionId = "SessionId",
            FeatureName = "FeatureName"
        }
    }
}
declare namespace _Ext {
    namespace AuditLogService {
        const baseUrl = "_Ext/AuditLog";
        function Create(request: Serenity.SaveRequest<AuditLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<AuditLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<AuditLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<AuditLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Create = "_Ext/AuditLog/Create",
            Update = "_Ext/AuditLog/Update",
            Delete = "_Ext/AuditLog/Delete",
            Retrieve = "_Ext/AuditLog/Retrieve",
            List = "_Ext/AuditLog/List"
        }
    }
}
declare namespace _Ext {
    interface AuditLogViewerRequest extends Serenity.ServiceRequest {
        FormKey?: string;
        EntityId?: number;
    }
}
declare namespace _Ext {
    interface AuditLogViewerResponse extends Serenity.ServiceResponse {
        EntityVersions?: AuditLogRow[];
    }
}
declare namespace _Ext {
    namespace AuditLogViewerService {
        const baseUrl = "AuditLogViewer";
        function List(request: AuditLogViewerRequest, onSuccess?: (response: AuditLogViewerResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            List = "AuditLogViewer/List"
        }
    }
}
declare namespace _Ext.DevTools {
    interface SergenConnection {
        Key?: string;
    }
}
declare namespace _Ext.DevTools {
    interface SergenGenerateOptions {
        Row?: boolean;
        Service?: boolean;
        UI?: boolean;
    }
}
declare namespace _Ext.DevTools {
    interface SergenGenerateRequest extends Serenity.ServiceRequest {
        ConnectionKey?: string;
        Table?: SergenTable;
        GenerateOptions?: SergenGenerateOptions;
    }
}
declare namespace _Ext.DevTools {
    interface SergenListTablesRequest extends Serenity.ServiceRequest {
        ConnectionKey?: string;
    }
}
declare namespace _Ext.DevTools {
    namespace SergenService {
        const baseUrl = "Administration/Sergen";
        function ListConnections(request: Serenity.ServiceRequest, onSuccess?: (response: Serenity.ListResponse<SergenConnection>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ListTables(request: SergenListTablesRequest, onSuccess?: (response: Serenity.ListResponse<SergenTable>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Generate(request: SergenGenerateRequest, onSuccess?: (response: Serenity.ServiceResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            ListConnections = "Administration/Sergen/ListConnections",
            ListTables = "Administration/Sergen/ListTables",
            Generate = "Administration/Sergen/Generate"
        }
    }
}
declare namespace _Ext.DevTools {
    interface SergenTable {
        Tablename?: string;
        Identifier?: string;
        Module?: string;
        PermissionKey?: string;
    }
}
declare namespace _Ext {
    interface EntityReportRequest extends Serenity.RetrieveRequest {
        ReportKey?: string;
        ReportServiceMethodName?: string;
        ReportDesignPath?: string;
    }
}
declare namespace _Ext {
    interface ListReportRequest extends Serenity.ListRequest {
        ReportKey?: string;
        ReportServiceMethodName?: string;
        ListExcelServiceMethodName?: string;
        ReportDesignPath?: string;
        EqualityFilterWithTextValue?: {
            [key: string]: string;
        };
        CustomParameters?: {
            [key: string]: any;
        };
    }
}
declare namespace _Ext {
    enum Months {
        January = 0,
        February = 1,
        March = 2,
        April = 3,
        May = 4,
        June = 5,
        July = 6,
        August = 7,
        September = 8,
        October = 9,
        November = 10,
        December = 11
    }
}
declare namespace _Ext {
    interface ReplaceRowForm {
        DeletedEntityName: Serenity.StringEditor;
        ReplaceWithEntityId: EmptyLookupEditor;
    }
    class ReplaceRowForm extends Serenity.PrefixedContext {
        static formKey: string;
        private static init;
        constructor(prefix: string);
    }
}
declare namespace _Ext {
    interface ReplaceRowRequest extends Serenity.ServiceRequest {
        FormKey?: string;
        IdProperty?: string;
        NameProperty?: string;
        EntityTypeTitle?: string;
        DeletedEntityId?: number;
        DeletedEntityName?: string;
        ReplaceWithEntityId?: number;
    }
}
declare namespace _Ext {
    interface ReplaceRowResponse extends Serenity.ServiceResponse {
    }
}
declare namespace _Ext {
    namespace ReplaceRowService {
        const baseUrl = "ReplaceRow";
        function Replace(request: ReplaceRowRequest, onSuccess?: (response: ReplaceRowResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        const enum Methods {
            Replace = "ReplaceRow/Replace"
        }
    }
}
declare namespace _Ext {
    enum TimeUoM {
        Hour = 1,
        Day = 2,
        Week = 3,
        Month = 4,
        CalenderMonth = 5,
        Year = 6
    }
}
declare namespace BMS_Scheduler.Administration {
    class LanguageDialog extends _Ext.DialogBase<LanguageRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: LanguageForm;
    }
}
declare namespace BMS_Scheduler.Administration {
    class LanguageGrid extends _Ext.GridBase<LanguageRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof LanguageDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): LanguageRow.Fields[];
    }
}
declare namespace BMS_Scheduler.Administration {
    class RoleDialog extends _Ext.DialogBase<RoleRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: RoleForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace BMS_Scheduler.Administration {
    class RoleGrid extends _Ext.GridBase<RoleRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof RoleDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): RoleRow.Fields[];
    }
}
declare namespace BMS_Scheduler.Administration {
    class RolePermissionDialog extends Serenity.TemplatedDialog<RolePermissionDialogOptions> {
        private permissions;
        constructor(opt: RolePermissionDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface RolePermissionDialogOptions {
        roleID?: number;
        title?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    class TranslationGrid extends Serenity.EntityGrid<TranslationItem, any> {
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        private hasChanges;
        private searchText;
        private sourceLanguage;
        private targetLanguage;
        private targetLanguageKey;
        constructor(container: JQuery);
        protected onClick(e: JQueryEventObject, row: number, cell: number): any;
        protected getColumns(): Slick.Column[];
        protected createToolbarExtensions(): void;
        protected saveChanges(language: string): PromiseLike<any>;
        protected onViewSubmit(): boolean;
        protected getButtons(): Serenity.ToolButton[];
        protected createQuickSearchInput(): void;
        protected onViewFilter(item: TranslationItem): boolean;
        protected usePager(): boolean;
    }
}
declare namespace BMS_Scheduler.Administration {
    class UserDialog extends _Ext.DialogBase<UserRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getIsActiveProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: UserForm;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace BMS_Scheduler.Administration {
    class UserGrid extends _Ext.GridBase<UserRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof UserDialog;
        protected getIdProperty(): string;
        protected getIsActiveProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): UserRow.Fields[];
    }
}
declare namespace BMS_Scheduler.Authorization {
    let userDefinition: ScriptUserDefinition;
    function hasPermission(permissionKey: string): boolean;
}
declare namespace BMS_Scheduler.Administration {
    class PermissionCheckEditor extends Serenity.DataGrid<PermissionCheckItem, PermissionCheckEditorOptions> {
        protected getIdProperty(): string;
        private searchText;
        private byParentKey;
        constructor(container: JQuery, opt: PermissionCheckEditorOptions);
        private getItemGrantRevokeClass;
        private roleOrImplicit;
        private getItemEffectiveClass;
        protected getColumns(): Slick.Column[];
        setItems(items: PermissionCheckItem[]): void;
        protected onViewSubmit(): boolean;
        protected onViewFilter(item: PermissionCheckItem): boolean;
        private matchContains;
        private getDescendants;
        protected onClick(e: any, row: any, cell: any): void;
        private getParentKey;
        protected getButtons(): Serenity.ToolButton[];
        protected createToolbarExtensions(): void;
        private getSortedGroupAndPermissionKeys;
        get value(): UserPermissionRow[];
        set value(value: UserPermissionRow[]);
        private _rolePermissions;
        get rolePermissions(): string[];
        set rolePermissions(value: string[]);
        private _implicitPermissions;
        set implicitPermissions(value: Q.Dictionary<string[]>);
    }
    interface PermissionCheckEditorOptions {
        showRevoke?: boolean;
    }
    interface PermissionCheckItem {
        ParentKey?: string;
        Key?: string;
        Title?: string;
        IsGroup?: boolean;
        GrantRevoke?: boolean;
    }
}
declare namespace BMS_Scheduler.Administration {
    class UserPermissionDialog extends Serenity.TemplatedDialog<UserPermissionDialogOptions> {
        private permissions;
        constructor(opt: UserPermissionDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface UserPermissionDialogOptions {
        userID?: number;
        username?: string;
    }
}
declare namespace BMS_Scheduler.Administration {
    class RoleCheckEditor extends Serenity.CheckTreeEditor<Serenity.CheckTreeItem<any>, any> {
        private searchText;
        constructor(div: JQuery);
        protected createToolbarExtensions(): void;
        protected getButtons(): any[];
        protected getTreeItems(): Serenity.CheckTreeItem<any>[];
        protected onViewFilter(item: any): boolean;
    }
}
declare namespace BMS_Scheduler.Administration {
    class UserRoleDialog extends Serenity.TemplatedDialog<UserRoleDialogOptions> {
        private permissions;
        constructor(opt: UserRoleDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface UserRoleDialogOptions {
        userID: number;
        username: string;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoDialog extends _Ext.DialogBase<CompanyInfoRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof CompanyInfoRow;
        protected getService(): string;
        protected form: CompanyInfoForm;
        constructor(options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoGrid extends _Ext.GridBase<CompanyInfoRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CompanyInfoDialog;
        protected getRowType(): typeof CompanyInfoRow;
        protected getService(): string;
        constructor(container: JQuery, options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoEditorDialog extends _Ext.EditorDialogBase<CompanyInfoRow> {
        protected getFormKey(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected form: CompanyInfoEditorForm;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CompanyInfoGridEditor extends _Ext.GridEditorBase<CompanyInfoRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CompanyInfoEditorDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryDialog extends _Ext.DialogBase<CountryRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof CountryRow;
        protected getService(): string;
        protected form: CountryForm;
        constructor(options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryGrid extends _Ext.GridBase<CountryRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CountryDialog;
        protected getRowType(): typeof CountryRow;
        protected getService(): string;
        constructor(container: JQuery, options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryEditorDialog extends _Ext.EditorDialogBase<CountryRow> {
        protected getFormKey(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected form: CountryEditorForm;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class CountryGridEditor extends _Ext.GridEditorBase<CountryRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CountryEditorDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingDialog extends _Ext.DialogBase<ShippingRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof ShippingRow;
        protected getService(): string;
        protected form: ShippingForm;
        constructor(options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingGrid extends _Ext.GridBase<ShippingRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ShippingDialog;
        protected getRowType(): typeof ShippingRow;
        protected getService(): string;
        constructor(container: JQuery, options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingEditorDialog extends _Ext.EditorDialogBase<ShippingRow> {
        protected getFormKey(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected form: ShippingEditorForm;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingGridEditor extends _Ext.GridEditorBase<ShippingRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ShippingEditorDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsDialog extends _Ext.DialogBase<ShippingItemsRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof ShippingItemsRow;
        protected getService(): string;
        protected form: ShippingItemsForm;
        constructor(options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsGrid extends _Ext.GridBase<ShippingItemsRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ShippingItemsDialog;
        protected getRowType(): typeof ShippingItemsRow;
        protected getService(): string;
        constructor(container: JQuery, options: any);
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsEditorDialog extends _Ext.EditorDialogBase<ShippingItemsRow> {
        protected getFormKey(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected form: ShippingItemsEditorForm;
    }
}
declare namespace BMS_Scheduler.BhasaniTask {
    class ShippingItemsGridEditor extends _Ext.GridEditorBase<ShippingItemsRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ShippingItemsEditorDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
    }
}
declare namespace BmsUtility {
    function GetTime(value: number): string;
    function GetTimeCode(value: number, frame: number): string;
    function pad(pad: any, str: any, padLeft: any): any;
    function ConvertToSecond(val: string): number;
    function getLongDate(val: Date): string;
    function getDates(start: Date, end: Date): any[];
    function getDatesFromString(commaSepDates: string): Date[];
    function CombineDateAndTime(date: Date, timeString: string): string;
}
declare namespace BMS_Scheduler.LanguageList {
    function getValue(): string[][];
}
declare namespace BMS_Scheduler.ScriptInitialization {
}
declare function usingAgGrid(): void;
declare function usingTabulator(): void;
declare function usingSlickCellMenu(): void;
declare function usingSplitJs(): void;
declare function usingResizable(): void;
declare namespace _Ext {
    class MultiDatePickerEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        private $container;
        constructor($input: JQuery);
        destroy(): void;
        toDate: (dateStr: any) => Date;
        get value(): string;
        set value(val: string);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        set_minValue(value: string): void;
        set_maxValue(value: string): void;
    }
}
declare namespace _Ext {
    class TimePickerEditor extends Serenity.TemplatedWidget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        protected getTemplate(): string;
        constructor(container: JQuery);
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
    }
}
declare namespace BMS_Scheduler.Common {
    class SidebarSearch extends Serenity.Widget<any> {
        private menuUL;
        constructor(input: JQuery, menuUL: JQuery);
        protected updateMatchFlags(text: string): void;
    }
}
declare namespace BMS_Scheduler.Common {
    interface ExcelExportOptions {
        grid: Serenity.DataGrid<any, any>;
        service: string;
        onViewSubmit: () => boolean;
        title?: string;
        hint?: string;
        separator?: boolean;
    }
    namespace ExcelExportHelper {
        function createToolButton(options: ExcelExportOptions): Serenity.ToolButton;
    }
}
declare var jsPDF: any;
declare namespace BMS_Scheduler.Common {
    interface PdfExportOptions {
        grid: Serenity.DataGrid<any, any>;
        onViewSubmit: () => boolean;
        title?: string;
        hint?: string;
        separator?: boolean;
        reportTitle?: string;
        titleTop?: number;
        titleFontSize?: number;
        fileName?: string;
        pageNumbers?: boolean;
        columnTitles?: {
            [key: string]: string;
        };
        tableOptions?: jsPDF.AutoTableOptions;
        output?: string;
        autoPrint?: boolean;
        printDateTimeHeader?: boolean;
    }
    namespace PdfExportHelper {
        function exportToPdf(options: PdfExportOptions): void;
        function createToolButton(options: PdfExportOptions): Serenity.ToolButton;
    }
}
declare var jsPDF: any;
declare namespace BMS_Scheduler.Common {
    class ReportDialog extends Serenity.TemplatedDialog<ReportDialogOptions> {
        private report;
        private propertyGrid;
        constructor(options: ReportDialogOptions);
        protected getDialogButtons(): any;
        protected createPropertyGrid(): void;
        protected loadReport(reportKey: string): void;
        protected updateInterface(): void;
        executeReport(target: string, ext: string, download: boolean): void;
        getToolbarButtons(): {
            title: string;
            cssClass: string;
            onClick: () => void;
        }[];
    }
    interface ReportDialogOptions {
        reportKey: string;
    }
}
declare namespace BMS_Scheduler.Common {
    interface ReportExecuteOptions {
        reportKey: string;
        download?: boolean;
        extension?: 'pdf' | 'htm' | 'html' | 'xlsx' | 'docx';
        getParams?: () => any;
        params?: {
            [key: string]: any;
        };
        target?: string;
    }
    interface ReportButtonOptions extends ReportExecuteOptions {
        title?: string;
        cssClass?: string;
        icon?: string;
    }
    namespace ReportHelper {
        function createToolButton(options: ReportButtonOptions): Serenity.ToolButton;
        function execute(options: ReportExecuteOptions): void;
    }
}
declare var jsPDF: any;
declare namespace BMS_Scheduler.Common {
    class ReportPage extends Serenity.Widget<any> {
        private reportKey;
        private propertyItems;
        private propertyGrid;
        constructor(element: JQuery);
        protected updateMatchFlags(text: string): void;
        protected categoryClick(e: any): void;
        protected reportLinkClick(e: any): void;
    }
}
declare namespace BMS_Scheduler.Membership {
    class LoginPanel extends Serenity.PropertyPanel<LoginRequest, any> {
        protected getTemplateName(): string;
        protected getFormKey(): string;
        constructor(container: JQuery);
        protected redirectToReturnUrl(): void;
        protected getTemplate(): string;
    }
}
declare namespace BMS_Scheduler.Membership {
    class ChangePasswordPanel extends Serenity.PropertyPanel<ChangePasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
        getTemplate(): string;
    }
}
declare namespace BMS_Scheduler.Membership {
    class ForgotPasswordPanel extends Serenity.PropertyPanel<ForgotPasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace BMS_Scheduler.Membership {
    class ResetPasswordPanel extends Serenity.PropertyPanel<ResetPasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace BMS_Scheduler.Membership {
    class SignUpPanel extends Serenity.PropertyPanel<SignUpRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace _Ext {
    class AuditLogActionTypeFormatter implements Slick.Formatter {
        static format(ctx: Slick.FormatterContext): string;
        format(ctx: Slick.FormatterContext): string;
    }
}
declare namespace _Ext {
    class AuditLogDialog extends DialogBase<AuditLogRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof AuditLogRow;
        protected getService(): string;
        protected form: AuditLogForm;
        constructor();
        protected afterLoadEntity(): void;
        static getChangesInHtml(changesInJson: string): string;
    }
}
declare namespace _Ext {
    class AuditLogGrid extends GridBase<AuditLogRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof AuditLogDialog;
        protected getRowType(): typeof AuditLogRow;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[];
    }
}
declare namespace _Ext {
    class AuditLogViewer {
        el: string;
        data: {
            entityVersions: any[];
        };
        entity: any;
        entityId: any;
        constructor(el: string, entityVersions: AuditLogRow[]);
        mounted: () => void;
        computed: {
            test: () => string;
        };
        filters: {
            filterByYardId: () => any[];
        };
        methods: {
            getDiff: (versionInfo: AuditLogRow) => string;
        };
        destroyed(): void;
    }
}
declare namespace _Ext {
    class AuditLogViewerDialog extends Serenity.TemplatedDialog<any> {
        request: AuditLogViewerRequest;
        constructor(request: AuditLogViewerRequest);
        protected getTemplateName(): string;
    }
}
declare namespace _Ext {
    class DialogSnippets extends DialogBase<AuditLogRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof AuditLogRow;
        protected getService(): string;
        protected form: AuditLogForm;
        protected addCssClass(): void;
        protected getTemplate(): string;
        protected getTemplateName(): string;
        protected getFallbackTemplate(): string;
        protected initValidator(): void;
        protected getValidatorOptions(): JQueryValidation.ValidationOptions;
        protected initTabs(): void;
        protected initToolbar(): void;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected initPropertyGrid(): void;
        protected getPropertyGridOptions(): Serenity.PropertyGridOptions;
        protected initLocalizationGrid(): void;
        protected initLocalizationGridCommon(pgOptions: Serenity.PropertyGridOptions): void;
        load(entityOrId: any, done: () => void, fail: (ex: Q.Exception) => void): void;
        loadResponse(data: any): void;
        protected onLoadingData(data: Serenity.RetrieveResponse<AuditLogRow>): void;
        protected beforeLoadEntity(entity: AuditLogRow): void;
        protected loadEntity(entity: AuditLogRow): void;
        protected set_entityId(value: any): void;
        protected set_entity(entity: any): void;
        protected isEditMode(): boolean;
        protected get_entityId(): any;
        protected get_entity(): AuditLogRow;
        protected afterLoadEntity(): void;
        protected updateInterface(): void;
        protected isDeleted(): boolean;
        protected isLocalizationMode(): boolean;
        protected isNew(): boolean;
        protected updateTitle(): void;
        protected getEntityTitle(): string;
        protected getEntitySingular(): string;
        protected getSaveEntity(): AuditLogRow;
        protected initDialog(): void;
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getDialogTitle(): string;
        protected handleResponsive(): void;
        protected onDialogOpen(): void;
        protected arrange(): void;
        protected save(callback?: (response: Serenity.SaveResponse) => void): void | boolean;
        protected validateBeforeSave(): boolean;
        protected save_submitHandler(callback: (response: Serenity.SaveResponse) => void): void;
        protected getSaveOptions(callback: (response: Serenity.SaveResponse) => void): Serenity.ServiceOptions<Serenity.SaveResponse>;
        protected getSaveRequest(): Serenity.SaveRequest<AuditLogRow>;
        protected saveHandler(options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void): void;
        protected onSaveSuccess(response: Serenity.SaveResponse): void;
        loadById(id: any, callback?: (response: Serenity.RetrieveResponse<AuditLogRow>) => void, fail?: () => void): void;
        protected getLoadByIdRequest(id: any): Serenity.RetrieveRequest;
        protected getLoadByIdOptions(id: any, callback: (response: Serenity.RetrieveResponse<AuditLogRow>) => void): Serenity.ServiceOptions<Serenity.RetrieveResponse<AuditLogRow>>;
        protected loadByIdHandler(options: Serenity.ServiceOptions<Serenity.RetrieveResponse<AuditLogRow>>, callback: (response: Serenity.RetrieveResponse<AuditLogRow>) => void, fail: () => void): void;
        protected showSaveSuccessMessage(response: Serenity.SaveResponse): void;
        protected getEntityNameFieldValue(): any;
        protected isCloneMode(): boolean;
        protected isNewOrDeleted(): boolean;
        protected getDeleteOptions(callback: (response: Serenity.DeleteResponse) => void): Serenity.ServiceOptions<Serenity.DeleteResponse>;
        protected deleteHandler(options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void): void;
        protected doDelete(callback: (response: Serenity.DeleteResponse) => void): void;
        protected onDeleteSuccess(response: Serenity.DeleteResponse): void;
        protected getEntityType(): string;
        protected getLocalTextDbPrefix(): string;
        protected getIsActiveProperty(): string;
        protected getIsDeletedProperty(): string;
        loadNewAndOpenDialog(asPanel?: boolean): void;
        loadEntityAndOpenDialog(entity: AuditLogRow, asPanel?: boolean): void;
        loadByIdAndOpenDialog(entityId: any, asPanel?: boolean): void;
        protected reloadById(): void;
        protected isLocalizationModeAndChanged(): boolean;
        protected localizationButtonClick(): void;
        protected getLanguages(): any[];
        protected loadLocalization(): void;
        protected setLocalizationGridCurrentValues(): void;
        protected getLocalizationGridValue(): any;
        protected getPendingLocalizations(): any;
        protected getPropertyItems(): Serenity.PropertyItem[];
        protected getCloningEntity(): AuditLogRow;
        protected getUndeleteOptions(callback?: (response: Serenity.UndeleteResponse) => void): Serenity.ServiceOptions<Serenity.UndeleteResponse>;
        protected undeleteHandler(options: Serenity.ServiceOptions<Serenity.UndeleteResponse>, callback: (response: Serenity.UndeleteResponse) => void): void;
        protected undelete(callback?: (response: Serenity.UndeleteResponse) => void): void;
        protected resetValidation(): void;
        protected validateForm(): boolean;
        protected onDialogClose(): void;
        destroy(): void;
    }
}
declare namespace _Ext {
    class DialogWithAllOverridableMethods extends DialogBase<AuditLogRow, any> {
        protected getFormKey(): string;
        protected getRowType(): typeof AuditLogRow;
        protected getService(): string;
        protected form: AuditLogForm;
        protected addCssClass(): void;
        protected getTemplate(): string;
        protected getTemplateName(): string;
        protected getFallbackTemplate(): string;
        protected initValidator(): void;
        protected getValidatorOptions(): JQueryValidation.ValidationOptions;
        protected initTabs(): void;
        protected initToolbar(): void;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected initPropertyGrid(): void;
        protected getPropertyGridOptions(): Serenity.PropertyGridOptions;
        protected initLocalizationGrid(): void;
        protected initLocalizationGridCommon(pgOptions: Serenity.PropertyGridOptions): void;
        load(entityOrId: any, done: () => void, fail: (ex: Q.Exception) => void): void;
        loadResponse(data: any): void;
        protected onLoadingData(data: Serenity.RetrieveResponse<AuditLogRow>): void;
        protected beforeLoadEntity(entity: AuditLogRow): void;
        protected loadEntity(entity: AuditLogRow): void;
        protected set_entityId(value: any): void;
        protected set_entity(entity: any): void;
        protected isEditMode(): boolean;
        protected get_entityId(): any;
        protected get_entity(): AuditLogRow;
        protected afterLoadEntity(): void;
        protected updateInterface(): void;
        protected isDeleted(): boolean;
        protected isLocalizationMode(): boolean;
        protected isNew(): boolean;
        protected updateTitle(): void;
        protected getEntityTitle(): string;
        protected getEntitySingular(): string;
        protected getSaveEntity(): AuditLogRow;
        protected initDialog(): void;
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getDialogTitle(): string;
        protected handleResponsive(): void;
        protected onDialogOpen(): void;
        protected arrange(): void;
        protected save(callback?: (response: Serenity.SaveResponse) => void): void | boolean;
        protected validateBeforeSave(): boolean;
        protected save_submitHandler(callback: (response: Serenity.SaveResponse) => void): void;
        protected getSaveOptions(callback: (response: Serenity.SaveResponse) => void): Serenity.ServiceOptions<Serenity.SaveResponse>;
        protected getSaveRequest(): Serenity.SaveRequest<AuditLogRow>;
        protected saveHandler(options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void): void;
        protected onSaveSuccess(response: Serenity.SaveResponse): void;
        loadById(id: any, callback?: (response: Serenity.RetrieveResponse<AuditLogRow>) => void, fail?: () => void): void;
        protected getLoadByIdRequest(id: any): Serenity.RetrieveRequest;
        protected getLoadByIdOptions(id: any, callback: (response: Serenity.RetrieveResponse<AuditLogRow>) => void): Serenity.ServiceOptions<Serenity.RetrieveResponse<AuditLogRow>>;
        protected loadByIdHandler(options: Serenity.ServiceOptions<Serenity.RetrieveResponse<AuditLogRow>>, callback: (response: Serenity.RetrieveResponse<AuditLogRow>) => void, fail: () => void): void;
        protected showSaveSuccessMessage(response: Serenity.SaveResponse): void;
        protected getEntityNameFieldValue(): any;
        protected isCloneMode(): boolean;
        protected isNewOrDeleted(): boolean;
        protected getDeleteOptions(callback: (response: Serenity.DeleteResponse) => void): Serenity.ServiceOptions<Serenity.DeleteResponse>;
        protected deleteHandler(options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void): void;
        protected doDelete(callback: (response: Serenity.DeleteResponse) => void): void;
        protected onDeleteSuccess(response: Serenity.DeleteResponse): void;
        protected getEntityType(): string;
        protected getLocalTextDbPrefix(): string;
        protected getIsActiveProperty(): string;
        protected getIsDeletedProperty(): string;
        loadNewAndOpenDialog(asPanel?: boolean): void;
        loadEntityAndOpenDialog(entity: AuditLogRow, asPanel?: boolean): void;
        loadByIdAndOpenDialog(entityId: any, asPanel?: boolean): void;
        protected reloadById(): void;
        protected isLocalizationModeAndChanged(): boolean;
        protected localizationButtonClick(): void;
        protected getLanguages(): any[];
        protected loadLocalization(): void;
        protected setLocalizationGridCurrentValues(): void;
        protected getLocalizationGridValue(): any;
        protected getPendingLocalizations(): any;
        protected getPropertyItems(): Serenity.PropertyItem[];
        protected getCloningEntity(): AuditLogRow;
        protected getUndeleteOptions(callback?: (response: Serenity.UndeleteResponse) => void): Serenity.ServiceOptions<Serenity.UndeleteResponse>;
        protected undeleteHandler(options: Serenity.ServiceOptions<Serenity.UndeleteResponse>, callback: (response: Serenity.UndeleteResponse) => void): void;
        protected undelete(callback?: (response: Serenity.UndeleteResponse) => void): void;
        protected resetValidation(): void;
        protected validateForm(): boolean;
        protected onDialogClose(): void;
        destroy(): void;
    }
}
declare namespace _Ext {
    class GridSnippets extends _Ext.GridBase<AuditLogRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof DialogSnippets;
        protected getRowType(): typeof AuditLogRow;
        protected getService(): string;
        protected get_ExtGridOptions(): ExtGridOptions;
        constructor(container: JQuery, options?: any);
        protected getInitialTitle(): string;
        protected getDisplayName(): string;
        setTitle(value: string): void;
        getTitle(): string;
        protected layout(): void;
        protected getButtons(): Serenity.ToolButton[];
        protected getAddButtonCaption(): string;
        protected getItemName(): string;
        protected newRefreshButton(noText?: boolean): Serenity.ToolButton;
        getView(): Slick.RemoteView<AuditLogRow>;
        protected createToolbar(buttons: Serenity.ToolButton[]): void;
        protected createSlickContainer(): JQuery;
        protected createView(): Slick.RemoteView<AuditLogRow>;
        protected getViewOptions(): Slick.RemoteViewOptions;
        protected getDefaultSortBy(): any[];
        protected usePager(): boolean;
        protected createSlickGrid(): Slick.Grid;
        protected getColumns(): Slick.Column[];
        protected getPropertyItems(): Serenity.PropertyItem[];
        protected propertyItemsToSlickColumns(propertyItems: Serenity.PropertyItem[]): Slick.Column[];
        protected itemLink(itemType?: string, idField?: string, text?: (ctx: Slick.FormatterContext) => string, cssClass?: (ctx: Slick.FormatterContext) => string, encode?: boolean): Slick.Format;
        protected getItemType(): string;
        protected getEntityType(): string;
        protected getSlickOptions(): Slick.GridOptions;
        postProcessColumns(columns: Slick.Column[]): Slick.Column[];
        protected setInitialSortOrder(): void;
        protected enableFiltering(): boolean;
        protected createFilterBar(): void;
        protected initializeFilterBar(): void;
        protected canFilterColumn(column: Slick.Column): boolean;
        protected createPager(): void;
        protected getPagerOptions(): Slick.PagerOptions;
        protected bindToSlickEvents(): void;
        protected bindToViewEvents(): void;
        protected createToolbarExtensions(): void;
        protected createIncludeDeletedButton(): void;
        protected createQuickSearchInput(): void;
        protected getQuickSearchFields(): Serenity.QuickSearchField[];
        protected createQuickFilters(): void;
        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[];
        protected dateTimeRangeQuickFilter(field: string, title?: string): Serenity.QuickFilter<Serenity.DateTimeEditor, Serenity.DateTimeEditorOptions>;
        protected addQuickFilter<TWidget extends Serenity.Widget<any>, TOptions>(opt: Serenity.QuickFilter<TWidget, TOptions>): TWidget;
        protected updateDisabledState(): void;
        protected getCurrentSettings(flags?: Serenity.GridPersistanceFlags): Serenity.PersistedGridSettings;
        protected gridPersistanceFlags(): Serenity.GridPersistanceFlags;
        protected restoreSettings(settings?: Serenity.PersistedGridSettings, flags?: Serenity.GridPersistanceFlags): void;
        protected getPersistedSettings(): Serenity.PersistedGridSettings;
        protected getPersistanceStorage(): Serenity.SettingStorage;
        getGrid(): Slick.Grid;
        protected initialPopulate(): void;
        protected populateWhenVisible(): boolean;
        protected onViewSubmit(): boolean;
        protected getGridCanLoad(): boolean;
        protected setCriteriaParameter(): void;
        protected setIncludeColumnsParameter(): void;
        protected getIncludeColumns(include: {
            [key: string]: boolean;
        }): void;
        protected invokeSubmitHandlers(): void;
        protected onViewProcessData(response: Serenity.ListResponse<AuditLogRow>): Serenity.ListResponse<AuditLogRow>;
        protected getItemMetadata(item: AuditLogRow, index: number): any;
        protected getItemCssClass(item: AuditLogRow, index: number): string;
        protected getIsActiveProperty(): string;
        protected getIsDeletedProperty(): string;
        protected onViewFilter(item: AuditLogRow): boolean;
        getElement(): JQuery;
        protected viewDataChanged(e: any, rows: AuditLogRow[]): void;
        protected markupReady(): void;
        getItems(): AuditLogRow[];
        setItems(value: AuditLogRow[]): void;
        protected addButtonClick(): void;
        protected editItem(entityOrId: any): void;
        protected editItemOfType(itemType: string, entityOrId: any): void;
        protected routeDialog(itemType: string, dialog: Serenity.Widget<any>): void;
        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>): void;
        protected createEntityDialog(itemType: string, callback?: (dlg: Serenity.Widget<any>) => void): Serenity.Widget<any>;
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getDialogOptionsFor(itemType: string): JQueryUI.DialogOptions;
        destroy(): void;
        protected onClick(e: JQueryEventObject, row: number, cell: number): void;
        protected setEquality(field: string, value: any): void;
        protected populateLock(): void;
        protected populateUnlock(): void;
        refresh(): void;
        protected refreshIfNeeded(): void;
        protected internalRefresh(): void;
        setIsDisabled(value: boolean): void;
        protected resizeCanvas(): void;
        protected subDialogDataChange(): void;
        protected addFilterSeparator(): void;
        protected determineText(getKey: (prefix: string) => string): string;
        protected addDateRangeFilter(field: string, title?: string): Serenity.DateEditor;
        protected dateRangeQuickFilter(field: string, title?: string): Serenity.QuickFilter<Serenity.DateEditor, Serenity.DateTimeEditorOptions>;
        protected addDateTimeRangeFilter(field: string, title?: string): Serenity.DateTimeEditor;
        protected addBooleanFilter(field: string, title?: string, yes?: string, no?: string): Serenity.SelectEditor;
        protected booleanQuickFilter(field: string, title?: string, yes?: string, no?: string): Serenity.QuickFilter<Serenity.SelectEditor, Serenity.SelectEditorOptions>;
        protected quickFilterChange(e: JQueryEventObject): void;
        protected getPersistanceKey(): string;
        protected canShowColumn(column: Slick.Column): boolean;
        protected persistSettings(flags?: Serenity.GridPersistanceFlags): void;
        getFilterStore(): Serenity.FilterStore;
    }
}
declare namespace _Ext {
    class GridWithAllOverridableMethods extends _Ext.GridBase<AuditLogRow, any> {
        protected getDialogType(): typeof DialogWithAllOverridableMethods;
        constructor(container: JQuery, options?: any);
        protected getInitialTitle(): string;
        protected getDisplayName(): string;
        protected getLocalTextPrefix(): string;
        setTitle(value: string): void;
        getTitle(): string;
        protected layout(): void;
        protected getButtons(): Serenity.ToolButton[];
        protected getAddButtonCaption(): string;
        protected getItemName(): string;
        protected newRefreshButton(noText?: boolean): Serenity.ToolButton;
        getView(): Slick.RemoteView<AuditLogRow>;
        protected createToolbar(buttons: Serenity.ToolButton[]): void;
        protected createSlickContainer(): JQuery;
        protected createView(): Slick.RemoteView<AuditLogRow>;
        protected getViewOptions(): Slick.RemoteViewOptions;
        protected getIdProperty(): string;
        protected getDefaultSortBy(): any[];
        protected usePager(): boolean;
        protected getService(): string;
        protected createSlickGrid(): Slick.Grid;
        protected getColumns(): Slick.Column[];
        protected getPropertyItems(): Serenity.PropertyItem[];
        protected getColumnsKey(): string;
        protected propertyItemsToSlickColumns(propertyItems: Serenity.PropertyItem[]): Slick.Column[];
        protected itemLink(itemType?: string, idField?: string, text?: (ctx: Slick.FormatterContext) => string, cssClass?: (ctx: Slick.FormatterContext) => string, encode?: boolean): Slick.Format;
        protected getItemType(): string;
        protected getEntityType(): string;
        protected getSlickOptions(): Slick.GridOptions;
        protected get_ExtGridOptions(): ExtGridOptions;
        postProcessColumns(columns: Slick.Column[]): Slick.Column[];
        protected setInitialSortOrder(): void;
        protected enableFiltering(): boolean;
        protected createFilterBar(): void;
        protected initializeFilterBar(): void;
        protected canFilterColumn(column: Slick.Column): boolean;
        protected createPager(): void;
        protected getPagerOptions(): Slick.PagerOptions;
        protected bindToSlickEvents(): void;
        protected bindToViewEvents(): void;
        protected createToolbarExtensions(): void;
        protected createIncludeDeletedButton(): void;
        protected createQuickSearchInput(): void;
        protected getQuickSearchFields(): Serenity.QuickSearchField[];
        protected createQuickFilters(): void;
        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[];
        protected dateTimeRangeQuickFilter(field: string, title?: string): Serenity.QuickFilter<Serenity.DateTimeEditor, Serenity.DateTimeEditorOptions>;
        protected addQuickFilter<TWidget extends Serenity.Widget<any>, TOptions>(opt: Serenity.QuickFilter<TWidget, TOptions>): TWidget;
        protected updateDisabledState(): void;
        protected getCurrentSettings(flags?: Serenity.GridPersistanceFlags): Serenity.PersistedGridSettings;
        protected gridPersistanceFlags(): Serenity.GridPersistanceFlags;
        protected restoreSettings(settings?: Serenity.PersistedGridSettings, flags?: Serenity.GridPersistanceFlags): void;
        protected getPersistedSettings(): Serenity.PersistedGridSettings;
        protected getPersistanceStorage(): Serenity.SettingStorage;
        getGrid(): Slick.Grid;
        protected initialPopulate(): void;
        protected populateWhenVisible(): boolean;
        protected onViewSubmit(): boolean;
        protected getGridCanLoad(): boolean;
        protected setCriteriaParameter(): void;
        protected setIncludeColumnsParameter(): void;
        protected getIncludeColumns(include: {
            [key: string]: boolean;
        }): void;
        protected invokeSubmitHandlers(): void;
        protected onViewProcessData(response: Serenity.ListResponse<AuditLogRow>): Serenity.ListResponse<AuditLogRow>;
        protected getItemMetadata(item: AuditLogRow, index: number): any;
        protected getItemCssClass(item: AuditLogRow, index: number): string;
        protected getIsActiveProperty(): string;
        protected getIsDeletedProperty(): string;
        protected onViewFilter(item: AuditLogRow): boolean;
        getElement(): JQuery;
        protected viewDataChanged(e: any, rows: AuditLogRow[]): void;
        protected markupReady(): void;
        getItems(): AuditLogRow[];
        setItems(value: AuditLogRow[]): void;
        protected addButtonClick(): void;
        protected editItem(entityOrId: any): void;
        protected editItemOfType(itemType: string, entityOrId: any): void;
        protected routeDialog(itemType: string, dialog: Serenity.Widget<any>): void;
        protected initEntityDialog(itemType: string, dialog: Serenity.Widget<any>): void;
        protected createEntityDialog(itemType: string, callback?: (dlg: Serenity.Widget<any>) => void): Serenity.Widget<any>;
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getDialogOptionsFor(itemType: string): JQueryUI.DialogOptions;
        destroy(): void;
        protected onClick(e: JQueryEventObject, row: number, cell: number): void;
        protected setEquality(field: string, value: any): void;
        protected populateLock(): void;
        protected populateUnlock(): void;
        refresh(): void;
        protected refreshIfNeeded(): void;
        protected internalRefresh(): void;
        setIsDisabled(value: boolean): void;
        protected resizeCanvas(): void;
        protected subDialogDataChange(): void;
        protected addFilterSeparator(): void;
        protected determineText(getKey: (prefix: string) => string): string;
        protected addDateRangeFilter(field: string, title?: string): Serenity.DateEditor;
        protected dateRangeQuickFilter(field: string, title?: string): Serenity.QuickFilter<Serenity.DateEditor, Serenity.DateTimeEditorOptions>;
        protected addDateTimeRangeFilter(field: string, title?: string): Serenity.DateTimeEditor;
        protected addBooleanFilter(field: string, title?: string, yes?: string, no?: string): Serenity.SelectEditor;
        protected booleanQuickFilter(field: string, title?: string, yes?: string, no?: string): Serenity.QuickFilter<Serenity.SelectEditor, Serenity.SelectEditorOptions>;
        protected quickFilterChange(e: JQueryEventObject): void;
        protected getPersistanceKey(): string;
        protected canShowColumn(column: Slick.Column): boolean;
        protected persistSettings(flags?: Serenity.GridPersistanceFlags): void;
        getFilterStore(): Serenity.FilterStore;
    }
}
declare namespace _Ext {
    class ReplaceRowDialog extends _Ext.DialogBase<any, any> {
        request: ReplaceRowRequest;
        entityList: Array<any>;
        protected getFormKey(): string;
        protected form: ReplaceRowForm;
        constructor(request: ReplaceRowRequest, entityList: Array<any>);
        protected getToolbarButtons(): Serenity.ToolButton[];
    }
}
declare var Vue: any;
declare namespace _Ext.DevTools {
    class SergenPanel extends Serenity.Widget<any> {
        constructor(container: JQuery);
    }
}
declare namespace _Ext {
    class AutoCompleteEditor extends Serenity.StringEditor {
        constructor(input: JQuery, options: AutoCompleteOptions);
        protected bindAutoComplete(input: any): void;
    }
    interface AutoCompleteOptions {
        lookupKey: string;
        sourceArray: string[];
        sourceCSV: string;
        minSearchLength: number;
    }
}
declare namespace _Ext {
    class AutoGeneratedCodeEditor extends Serenity.Widget<AutoGeneratedCodeEditorOptions> implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        private editorOption;
        constructor(container: JQuery, options: AutoGeneratedCodeEditorOptions);
        private Repeat;
        get value(): string;
        set value(val: string);
        private getMessage;
        set_readOnly(value: boolean): void;
    }
    interface AutoGeneratedCodeEditorOptions {
        Message?: string;
        CodeFor: BMS_Scheduler.Common.AutoGeneratedCodeConfiguration;
    }
}
declare namespace _Ext {
    class ColorEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        constructor(input: JQuery);
        get value(): number;
        set value(val: number);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
    }
}
declare namespace _Ext {
    class DateTimePickerEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        constructor(container: JQuery);
        get value(): string;
        set value(val: string);
        get valueAsDate(): Date;
        set valueAsDate(val: Date);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        set_minDate(date: Date): void;
        set_maxDate(date: Date): void;
        set_minDateTime(date: Date): void;
        set_maxDateTime(date: Date): void;
    }
}
declare namespace _Ext {
    class EmptyLookupEditor extends Serenity.LookupEditorBase<Serenity.LookupEditorOptions, any> {
        setSelect2Items(items: Select2Item[]): void;
        setLookupItems(lookup: Q.Lookup<any>): void;
    }
}
declare namespace _Ext {
    class HardCodedLookupEditor extends Serenity.Select2Editor<any, any> {
        constructor(container: JQuery, options: HardCodedLookupEditorOptions);
        protected getSelect2Options(): Select2Options;
    }
    interface HardCodedLookupEditorOptions {
        sourceArray: string[];
        sourceCSV: string;
        allowOtherValue: boolean;
    }
}
declare namespace _Ext {
    class HtmlTemplateEditor extends Serenity.HtmlContentEditor {
        constructor(textArea: JQuery, opt?: HtmlTemplateEditorOptions);
        protected getConfig(): Serenity.CKEditorConfig;
    }
    interface HtmlTemplateEditorOptions extends Serenity.HtmlContentEditorOptions {
        cols?: any;
        rows?: any;
        placeholders?: any;
    }
}
declare namespace _Ext {
    class JsonViewer extends Serenity.TemplatedWidget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        protected getTemplate(): string;
        private _value;
        set value(val: any);
        get value(): any;
    }
}
declare namespace _Ext {
    class MaskedTimeEditor extends Serenity.Widget<MaskedTimeEditorOptions> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        constructor(input: JQuery, options?: MaskedTimeEditorOptions);
        get value(): number;
        set value(val: number);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        private ConvertToSecond;
        private ConvertToTimeString;
    }
    interface MaskedTimeEditorOptions {
        ShowSeconds: boolean;
    }
}
declare namespace _Ext {
    class MonthYearEditor extends Serenity.Widget<any> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        constructor(container: JQuery);
        get value(): string;
        set value(val: string);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        set_minValue(value: string): void;
        set_maxValue(value: string): void;
    }
}
declare namespace _Ext {
    class PrefixedStringEditor extends Serenity.Widget<PrefixedStringEditorOptions> implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        private prefixInput;
        constructor(container: JQuery, options: PrefixedStringEditorOptions);
        get value(): string;
        set value(val: string);
        private _prefix;
        get prefix(): string;
        set prefix(val: string);
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
    }
    interface PrefixedStringEditorOptions {
        prefixLength: number;
        inputMaxLength: number;
        prefixFormatterType?: string;
    }
}
declare namespace _Ext {
    class StaticTextBlock extends Serenity.Widget<StaticTextBlockOptions> implements Serenity.ISetEditValue {
        private _value;
        constructor(container: JQuery, options: StaticTextBlockOptions);
        private updateElementContent;
        /**
         * By implementing ISetEditValue interface, we allow this editor to display its field value.
         * But only do this when our text content is not explicitly set in options
         */
        setEditValue(source: any, property: Serenity.PropertyItem): void;
        get value(): string;
        set value(value: string);
    }
    interface StaticTextBlockOptions {
        text: string;
        isHtml: boolean;
        isLocalText: boolean;
        hideLabel: boolean;
        isDate: boolean;
        isDateTime: boolean;
    }
}
declare namespace _Ext {
    class YesNoEditor extends Serenity.Select2Editor<any, any> {
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        constructor(container: JQuery);
        get valueAsBoolean(): boolean;
        set valueAsBoolean(val: boolean);
    }
}
declare namespace _Ext {
    class GridItemPickerEditor extends Serenity.Widget<GridItemPickerEditorOptions> implements Serenity.ISetEditValue, Serenity.IGetEditValue, Serenity.IStringValue, Serenity.IReadOnly, Serenity.IValidateRequired {
        options: GridItemPickerEditorOptions;
        containerDiv: JQuery;
        inplaceSearchButton: JQuery;
        inplaceViewButton: JQuery;
        clearSelectionButton: JQuery;
        constructor(container: JQuery, options: GridItemPickerEditorOptions);
        protected addInplaceButtons(): void;
        protected inplaceSearchClick(e: any): void;
        protected inplaceViewClick(e: any): void;
        private getDialogInstance;
        get value(): string;
        set value(val: string);
        get values(): string[];
        set values(val: string[]);
        get text(): string;
        set text(val: string);
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        get_value(): string;
        set_value(value: string): void;
        get_readOnly(): boolean;
        set_readOnly(value: boolean): void;
        get_required(): boolean;
        set_required(value: boolean): void;
        private _selectedItem;
        selectedItemIncludeColumns: string[];
        get selectedItem(): any;
        selectedItems: any[];
        private _serviceUrl;
        get serviceUrl(): string;
        setValueAndText(value: any, text: any): void;
        protected getCascadeFromValue(parent: Serenity.Widget<any>): any;
        protected cascadeLink: Serenity.CascadedWidgetLink<Serenity.Widget<any>>;
        protected setCascadeFrom(value: string): void;
        protected get_cascadeFrom(): string;
        get cascadeFrom(): string;
        protected set_cascadeFrom(value: string): void;
        set cascadeFrom(value: string);
        protected get_cascadeField(): any;
        get cascadeField(): string;
        protected set_cascadeField(value: string): void;
        set cascadeField(value: string);
        protected get_cascadeValue(): any;
        get cascadeValue(): any;
        protected set_cascadeValue(value: any): void;
        set cascadeValue(value: any);
        protected get_filterField(): string;
        get filterField(): string;
        protected set_filterField(value: string): void;
        set filterField(value: string);
        protected get_filterValue(): any;
        get filterValue(): any;
        protected set_filterValue(value: any): void;
        set filterValue(value: any);
        protected updateItems(): void;
    }
    interface GridItemPickerEditorOptions extends Serenity.Select2FilterOptions {
        gridType: any;
        nameFieldInThisRow?: string;
        serviceUrl?: string;
        rowType?: string;
        idFieldInGridRow?: string;
        nameFieldInGridRow?: string;
        inplaceView?: boolean;
        multiple?: boolean;
        preSelectedKeys?: any[];
        filteringCriteria?: any;
        customPrams?: any;
        dialogType?: any;
        cascadeFrom?: string;
        cascadeField?: string;
        cascadeValue?: any;
        filterField?: string;
        filterValue?: any;
    }
}
declare namespace _Ext {
    class InlineImageFormatter implements Slick.Formatter, Serenity.IInitializeColumn {
        format(ctx: Slick.FormatterContext): string;
        initializeColumn(column: Slick.Column): void;
        fileProperty: string;
        thumb: boolean;
        defaultImage: string;
        maxHeight: string;
        maxWidth: string;
    }
}
declare namespace _Ext {
    class InlineMultipleImageFormatter implements Slick.Formatter, Serenity.IInitializeColumn {
        format(ctx: Slick.FormatterContext): string;
        initializeColumn(column: Slick.Column): void;
        fileProperty: string;
        thumb: boolean;
        inlineUpload: boolean;
        defaultImage: string;
        maxHeight: string;
        maxWidth: string;
    }
}
declare namespace _Ext {
    class MaskedTimeFrameFormatter implements Slick.Formatter {
        static format(val: string): string;
        format(ctx: Slick.FormatterContext): string;
    }
}
declare namespace _Ext {
    class MonthYearFormatter implements Slick.Formatter {
        static format(val: string): string;
        format(ctx: Slick.FormatterContext): string;
    }
}
declare namespace _Ext {
    class YesNoColoredFormatter implements Slick.Formatter {
        static format(val: any): string;
        format(ctx: Slick.FormatterContext): string;
    }
}
declare namespace _Ext {
    class YesNoFormatter implements Slick.Formatter {
        static format(val: any): string;
        format(ctx: Slick.FormatterContext): string;
    }
}
declare namespace _Ext {
    class CardViewMixin<TItem> {
        private options;
        private dataGrid;
        private getId;
        private vm;
        private cardContainer;
        viewType: ('list' | 'card');
        constructor(options: CardViewMixinOptions<TItem>);
        switchView(viewType: ('grid' | 'card')): void;
        updateCardItems(): void;
        resizeCardView(): void;
    }
    interface CardViewMixinOptions<TItem> {
        grid: Serenity.DataGrid<TItem, any>;
        containerTemplate?: string;
        itemTemplate?: string;
        methods?: any;
        itemCssClass?: string;
        defaultViewType?: ('list' | 'card');
        itemsCssStyle?: string;
        itemCssStyle?: string;
    }
}
declare namespace _Ext {
    /**
     * A mixin that can be applied to a DataGrid for excel style filtering functionality
     */
    class HeaderFiltersMixin<TItem> {
        private options;
        private dataGrid;
        constructor(options: HeaderFiltersMixinOptions<TItem>);
    }
    interface HeaderFiltersMixinOptions<TItem> {
        grid: Serenity.DataGrid<TItem, any>;
    }
}
declare namespace _Ext {
    /**
     * A mixin that can be applied to a DataGrid for tree functionality
     */
    class TreeGridMixin<TItem> {
        private options;
        private dataGrid;
        private getId;
        constructor(options: TreeGridMixinOptions<TItem>);
        /**
         * Expands / collapses all rows in a grid automatically
         */
        toggleAll(): void;
        expandAll(): void;
        collapsedAll(): void;
        /**
         * Reorders a set of items so that parents comes before their children.
         * This method is required for proper tree ordering, as it is not so easy to perform with SQL.
         * @param items list of items to be ordered
         * @param getId a delegate to get ID of a record (must return same ID with grid identity field)
         * @param getParentId a delegate to get parent ID of a record
         */
        static applyTreeOrdering<TItem>(items: TItem[], getId: (item: TItem) => any, getParentId: (item: TItem) => any): TItem[];
        static filterById<TItem>(item: TItem, view: Slick.RemoteView<TItem>, idProperty: any, getParentId: (x: TItem) => any): boolean;
        static treeToggle<TItem>(getView: () => Slick.RemoteView<TItem>, getId: (x: TItem) => any, formatter: Slick.Format): Slick.Format;
        static toggleClick<TItem>(e: JQueryEventObject, row: number, cell: number, view: Slick.RemoteView<TItem>, getId: (x: TItem) => any): void;
    }
    interface TreeGridMixinOptions<TItem> {
        grid: Serenity.DataGrid<TItem, any>;
        idField?: string;
        getParentId: (item: TItem) => any;
        toggleField: string;
        initialCollapse?: () => boolean;
    }
}
declare namespace _Ext {
    interface ExcelExportOptions {
        grid: Serenity.DataGrid<any, any>;
        service: string;
        onViewSubmit: () => boolean;
        editRequest?: (request: Serenity.ListRequest) => Serenity.ListRequest;
        title?: string;
        hint?: string;
        separator?: boolean;
    }
    namespace ExcelExportHelper {
        function createToolButton(options: ExcelExportOptions): Serenity.ToolButton;
    }
}
declare var jsPDF: any;
declare namespace _Ext {
    interface PdfExportOptions {
        grid: Serenity.DataGrid<any, any>;
        onViewSubmit: () => boolean;
        title?: string;
        hint?: string;
        separator?: boolean;
        reportTitle?: string;
        titleTop?: number;
        titleFontSize?: number;
        fileName?: string;
        pageNumbers?: boolean;
        columnTitles?: {
            [key: string]: string;
        };
        tableOptions?: jsPDF.AutoTableOptions;
        output?: string;
        autoPrint?: boolean;
    }
    namespace PdfExportHelper {
        function exportToPdf(options: PdfExportOptions): void;
        function createToolButton(options: PdfExportOptions): Serenity.ToolButton;
    }
}
declare namespace Slick {
    interface RemoteView<TEntity> {
        getGroups(): Slick.Group<TEntity>[];
        getGrouping(): Slick.GroupInfo<TEntity>[];
    }
}
declare namespace _Ext {
    interface ReportExecuteOptions {
        reportKey: string;
        download?: boolean;
        extension?: 'pdf' | 'htm' | 'html' | 'xlsx' | 'docx';
        getParams?: () => any;
        params?: {
            [key: string]: any;
        };
        target?: string;
    }
    interface ReportButtonOptions extends ReportExecuteOptions {
        title?: string;
        cssClass?: string;
        icon?: string;
    }
    namespace ReportHelper {
        function createToolButton(options: ReportButtonOptions): Serenity.ToolButton;
        function execute(options: ReportExecuteOptions): void;
    }
}
declare namespace _Ext.DialogUtils {
    function pendingChangesConfirmation(element: JQuery, hasPendingChanges: () => boolean): void;
}
declare function loadScript(url: any): void;
declare function loadCss(url: any, styleId: any): void;
declare function usingVuejs(): void;
declare function usingBootstrapDatePicker(): void;
declare function usingBootstrapTimePicker(): void;
declare function usingBootstrapColorPicker(): void;
declare function usingJqueryUITimepickerAddon(): void;
declare function usingChartjs(): void;
declare function usingSlickGridEditors(): void;
declare function usingSlickAutoColumnSize(): void;
declare function usingSlickHeaderFilters(): void;
declare namespace q {
    function sum(xs: any[], key: any): number;
    function sortBy<T>(xs: T[], key: any): T[];
    function sortByDesc<T>(xs: T[], key: any): T[];
}
declare namespace q {
    function nextTick(date: any): Date;
    function addMinutes(date: Date, minutes: number): Date;
    function addHours(date: Date, hours: number): Date;
    function getHours(fromDate: Date, toDate: Date): number;
    function getDays24HourPulse(fromDate: Date, toDate: Date): number;
    function getDays(pFromDate: Date, pToDate: Date): number;
    function getMonths(fromDate: Date, toDate: Date): number;
    function getCalenderMonths(fromDate: Date, toDate: Date): number;
    function getCalenderMonthsCeil(fromDate: Date, toDate: Date): number;
    function addDays(date: Date, days: number): Date;
    function addMonths(date: Date, months: number): Date;
    function addYear(date: Date, years: number): Date;
    function getPeriods(fromDate: Date, toDate: Date, periodUnit: _Ext.TimeUoM): number;
    function addPeriod(date: Date, period: number, periodUnit: _Ext.TimeUoM): Date;
    function formatISODate(date: Date): string;
    function bindDateTimeEditorChange(editor: any, handler: any): void;
    function setMinDate(editor: Serenity.DateEditor | Serenity.DateTimeEditor, value: Date): void;
    function setMaxDate(editor: Serenity.DateEditor | Serenity.DateTimeEditor, value: Date): void;
    function initDateRangeEditor(fromDateEditor: Serenity.DateEditor, toDateEditor: Serenity.DateEditor, onChangeHandler?: (e: JQueryEventObject) => void): void;
    function initDateTimeRangeEditor(fromDateTimeEditor: _Ext.DateTimePickerEditor, toDateTimeEditor: _Ext.DateTimePickerEditor, onChangeHandler?: (e: JQueryEventObject) => void): void;
    function formatDate(d: Date | string, format?: string): string;
}
declare namespace q {
    function initDetailEditor(dialog: _Ext.DialogBase<any, any>, editor: _Ext.GridEditorBase<any>, options?: ExtGridEditorOptions): void;
    function setGridEditorHeight(editor: JQuery, heightInPx: number): void;
    function addNotificationIcon(editor: Serenity.Widget<any>, isSuccess: boolean): void;
    function addPopoverIcon(editor: Serenity.Widget<any>, isSuccess: boolean, popoverOptions: any): void;
    function setEditorLabel(editor: Serenity.Widget<any>, value: string): void;
    function hideEditorLabel(editor: Serenity.Widget<any>): void;
    function setEditorCategoryLabel(editor: Serenity.Widget<any>, value: string): void;
    function hideEditorCategory(editor: Serenity.Widget<any>, value?: boolean): void;
    function hideCategories(containerElement: JQuery, value?: boolean): void;
    function hideFields(containerElement: JQuery, value?: boolean): void;
    function MergeEditor(firstElement: Serenity.Widget<any>, secondElement: Serenity.Widget<any>): void;
    function hideFieldsAndCategories(containerElement: JQuery, value?: boolean): void;
    function hideField(editor: Serenity.Widget<any>, value?: boolean): void;
    function showField(editor: Serenity.Widget<any>, value?: boolean): void;
    function showAndEnableField(editor: Serenity.Widget<any>): void;
    function showFieldAndCategory(editor: Serenity.Widget<any>, value?: boolean): void;
    function hideEditorTab(editor: Serenity.Widget<any>, value?: boolean): void;
    function disableEditorTab(editor: Serenity.Widget<any>, value?: boolean): void;
    function readOnlyEditorTab(editor: Serenity.Widget<any>, value?: boolean): void;
    function readOnlyEditorCategory(editor: Serenity.Widget<any>, value?: boolean): void;
    function readonlyEditorCategory($editor: JQuery, value?: boolean): void;
    function readOnlyEditorPropertyGrid(editor: Serenity.Widget<any>, value?: boolean): void;
    function readonlyEditorPropertyGrid($editor: JQuery, value?: boolean): void;
    function readOnlyEditor(editor: Serenity.Widget<any>, value?: boolean): void;
    function readonlyEditor($editor: JQuery, value?: boolean): void;
    function moveEditorFromTab(editor: Serenity.Widget<any>, toElement: JQuery, isPrepend?: boolean): void;
    function moveEditorCategoryFromTab(editor: Serenity.Widget<any>, toElement: JQuery, isPrepend?: boolean): void;
    function selectEditorTab(editor: Serenity.Widget<any>): void;
}
declare namespace q {
    function getEnumText(enumTypeOrKey: any, value: any): string;
    function isNumber(value: any): boolean;
    function getEnumValues(enumType: any): number[];
    function getEnumKeys(enumType: any): string[];
}
declare namespace q {
    function switchKeybordLayout($container: any, layout: any): void;
}
declare namespace q {
    function text(key: string, fallback: string): string;
    function isCosmicThemeApplied(): boolean;
    function getSelectedLanguage(): string;
    function isBanglaMode(): boolean;
    function formatDecimal(value: any): string;
    function formatInt(value: any): string;
    function ToNumber(value: any): number;
    function ToFixed(value: any, fractionDigits?: number): string;
    function ToBool(value: any): boolean;
    function getRandomColor(hexLetters: any): string;
}
