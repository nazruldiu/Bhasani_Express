namespace BMS_Scheduler.Administration {
    export interface RoleRow {
        RoleId?: number;
        RoleName?: string;
    }

    export namespace RoleRow {
        export const idProperty = 'RoleId';
        export const nameProperty = 'RoleName';
        export const localTextPrefix = 'Administration.Role';
        export const lookupKey = 'Administration.Role';

        export function getLookup(): Q.Lookup<RoleRow> {
            return Q.getLookup<RoleRow>('Administration.Role');
        }
        export const deletePermission = 'Administration:Role:Delete';
        export const insertPermission = 'Administration:Role:Insert';
        export const readPermission = 'Administration:Role:Read';
        export const updatePermission = 'Administration:Role:Update';

        export declare const enum Fields {
            RoleId = "RoleId",
            RoleName = "RoleName"
        }
    }
}
