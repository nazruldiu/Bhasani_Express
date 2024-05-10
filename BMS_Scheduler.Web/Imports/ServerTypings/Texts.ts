namespace BMS_Scheduler.Texts {

    declare namespace Controls {
        export const DownloadPDF: string;

        namespace IdleTimeout {
            export const CountdownMessage: string;
            export const KeepAliveButton: string;
            export const SignoutButton: string;
            export const WarningMessage: string;
            export const WarningTitle: string;
        }
        export const View: string;
    }

    declare namespace Db {

        namespace Administration {

            namespace Language {
                export const Id: string;
                export const LanguageId: string;
                export const LanguageName: string;
            }

            namespace Role {
                export const RoleId: string;
                export const RoleName: string;
            }

            namespace RolePermission {
                export const PermissionKey: string;
                export const RoleId: string;
                export const RolePermissionId: string;
                export const RoleRoleName: string;
            }

            namespace Translation {
                export const CustomText: string;
                export const EntityPlural: string;
                export const Key: string;
                export const OverrideConfirmation: string;
                export const SaveChangesButton: string;
                export const SourceLanguage: string;
                export const SourceText: string;
                export const TargetLanguage: string;
                export const TargetText: string;
            }

            namespace User {
                export const DisplayName: string;
                export const Email: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const IsAdmin: string;
                export const LastDirectoryUpdate: string;
                export const Password: string;
                export const PasswordConfirm: string;
                export const PasswordHash: string;
                export const PasswordSalt: string;
                export const Source: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const UserId: string;
                export const UserImage: string;
                export const Username: string;
            }

            namespace UserPermission {
                export const Granted: string;
                export const PermissionKey: string;
                export const User: string;
                export const UserId: string;
                export const UserPermissionId: string;
                export const Username: string;
            }

            namespace UserRole {
                export const RoleId: string;
                export const User: string;
                export const UserId: string;
                export const UserRoleId: string;
                export const Username: string;
            }
        }

        namespace BhasaniTask {

            namespace CompanyInfo {
                export const Address: string;
                export const Fax: string;
                export const Id: string;
                export const Logo: string;
                export const Name: string;
                export const Phone: string;
                export const WebSite: string;
            }

            namespace Country {
                export const Id: string;
                export const Name: string;
            }

            namespace Shipping {
                export const Consignee: string;
                export const CountryOfOriginOfGoods: string;
                export const FinalDestination: string;
                export const Id: string;
                export const InVoiceDate: string;
                export const InVoiceNo: string;
                export const Measure: string;
                export const OrderNo: string;
                export const PackingSlipNo: string;
                export const PaymentTerms: string;
                export const ShippingItemList: string;
                export const TotalAmount: string;
                export const TotalBox: string;
                export const TotalWeight: string;
                export const VesselOrFlightNo: string;
            }

            namespace ShippingItems {
                export const DescriptionOfGoods: string;
                export const Id: string;
                export const Quantity: string;
                export const ShippingConsignee: string;
                export const ShippingCountryOfOriginOfGoods: string;
                export const ShippingFinalDestination: string;
                export const ShippingId: string;
                export const ShippingInVoiceDate: string;
                export const ShippingInVoiceNo: string;
                export const ShippingMeasure: string;
                export const ShippingOrderNo: string;
                export const ShippingPackingSlipNo: string;
                export const ShippingPaymentTerms: string;
                export const ShippingTotalAmount: string;
                export const ShippingTotalBox: string;
                export const ShippingTotalWeight: string;
                export const ShippingVesselOrFlightNo: string;
                export const TotalRate: string;
                export const Traiff: string;
                export const UnitRate: string;
                export const Uom: string;
            }
        }

        namespace _Ext {

            namespace AuditLog {
                export const ActionDate: string;
                export const ActionType: string;
                export const Changes: string;
                export const EntityId: string;
                export const EntityTableName: string;
                export const FeatureName: string;
                export const FormattedActionDate: string;
                export const Id: string;
                export const IpAddress: string;
                export const SessionId: string;
                export const UserId: string;
            }
        }
    }

    declare namespace Forms {

        namespace Membership {

            namespace ChangePassword {
                export const FormTitle: string;
                export const SubmitButton: string;
                export const Success: string;
            }

            namespace ForgotPassword {
                export const BackToLogin: string;
                export const FormInfo: string;
                export const FormTitle: string;
                export const SubmitButton: string;
                export const Success: string;
            }

            namespace Login {
                export const FacebookButton: string;
                export const ForgotPassword: string;
                export const GoogleButton: string;
                export const LogInButton: string;
                export const LoginToYourAccount: string;
                export const OR: string;
                export const RememberMe: string;
                export const SignInButton: string;
                export const SignUpButton: string;
            }

            namespace ResetPassword {
                export const BackToLogin: string;
                export const EmailSubject: string;
                export const FormTitle: string;
                export const SubmitButton: string;
                export const Success: string;
            }

            namespace SignUp {
                export const AcceptTerms: string;
                export const ActivateEmailSubject: string;
                export const ActivationCompleteMessage: string;
                export const BackToLogin: string;
                export const ConfirmEmail: string;
                export const ConfirmPassword: string;
                export const DisplayName: string;
                export const Email: string;
                export const FormInfo: string;
                export const FormTitle: string;
                export const Password: string;
                export const SubmitButton: string;
                export const Success: string;
            }
        }
    }

    declare namespace Navigation {
        export const LogoutLink: string;
        export const SiteTitle: string;
    }

    declare namespace Site {

        namespace AccessDenied {
            export const ClickToChangeUser: string;
            export const ClickToLogin: string;
            export const LackPermissions: string;
            export const NotLoggedIn: string;
            export const PageTitle: string;
        }

        namespace BasicProgressDialog {
            export const CancelTitle: string;
            export const PleaseWait: string;
        }

        namespace BulkServiceAction {
            export const AllHadErrorsFormat: string;
            export const AllSuccessFormat: string;
            export const ConfirmationFormat: string;
            export const ErrorCount: string;
            export const NothingToProcess: string;
            export const SomeHadErrorsFormat: string;
            export const SuccessCount: string;
        }

        namespace Dashboard {
            export const ContentDescription: string;
        }

        namespace Layout {
            export const FooterCopyright: string;
            export const FooterInfo: string;
            export const FooterRights: string;
            export const GeneralSettings: string;
            export const Language: string;
            export const Theme: string;
            export const ThemeBlack: string;
            export const ThemeBlackLight: string;
            export const ThemeBlue: string;
            export const ThemeBlueLight: string;
            export const ThemeGreen: string;
            export const ThemeGreenLight: string;
            export const ThemePurple: string;
            export const ThemePurpleLight: string;
            export const ThemeRed: string;
            export const ThemeRedLight: string;
            export const ThemeYellow: string;
            export const ThemeYellowLight: string;
        }

        namespace RolePermissionDialog {
            export const DialogTitle: string;
            export const EditButton: string;
            export const SaveSuccess: string;
        }

        namespace UserDialog {
            export const EditPermissionsButton: string;
            export const EditRolesButton: string;
        }

        namespace UserPermissionDialog {
            export const DialogTitle: string;
            export const Grant: string;
            export const Permission: string;
            export const Revoke: string;
            export const SaveSuccess: string;
        }

        namespace UserRoleDialog {
            export const DialogTitle: string;
            export const SaveSuccess: string;
        }

        namespace ValidationError {
            export const Title: string;
        }
    }

    declare namespace Validation {
        export const AuthenticationError: string;
        export const CantFindUserWithEmail: string;
        export const CurrentPasswordMismatch: string;
        export const DeleteForeignKeyError: string;
        export const EmailConfirm: string;
        export const EmailInUse: string;
        export const InvalidActivateToken: string;
        export const InvalidResetToken: string;
        export const MinRequiredPasswordLength: string;
        export const SavePrimaryKeyError: string;
        export const UsernameAndPasswordRequired: string;
    }

    BMS_Scheduler['Texts'] = Q.proxyTexts(Texts, '', {Controls:{DownloadPDF:1,IdleTimeout:{CountdownMessage:1,KeepAliveButton:1,SignoutButton:1,WarningMessage:1,WarningTitle:1},View:1},Db:{Administration:{Language:{Id:1,LanguageId:1,LanguageName:1},Role:{RoleId:1,RoleName:1},RolePermission:{PermissionKey:1,RoleId:1,RolePermissionId:1,RoleRoleName:1},Translation:{CustomText:1,EntityPlural:1,Key:1,OverrideConfirmation:1,SaveChangesButton:1,SourceLanguage:1,SourceText:1,TargetLanguage:1,TargetText:1},User:{DisplayName:1,Email:1,InsertDate:1,InsertUserId:1,IsActive:1,IsAdmin:1,LastDirectoryUpdate:1,Password:1,PasswordConfirm:1,PasswordHash:1,PasswordSalt:1,Source:1,UpdateDate:1,UpdateUserId:1,UserId:1,UserImage:1,Username:1},UserPermission:{Granted:1,PermissionKey:1,User:1,UserId:1,UserPermissionId:1,Username:1},UserRole:{RoleId:1,User:1,UserId:1,UserRoleId:1,Username:1}},BhasaniTask:{CompanyInfo:{Address:1,Fax:1,Id:1,Logo:1,Name:1,Phone:1,WebSite:1},Country:{Id:1,Name:1},Shipping:{Consignee:1,CountryOfOriginOfGoods:1,FinalDestination:1,Id:1,InVoiceDate:1,InVoiceNo:1,Measure:1,OrderNo:1,PackingSlipNo:1,PaymentTerms:1,ShippingItemList:1,TotalAmount:1,TotalBox:1,TotalWeight:1,VesselOrFlightNo:1},ShippingItems:{DescriptionOfGoods:1,Id:1,Quantity:1,ShippingConsignee:1,ShippingCountryOfOriginOfGoods:1,ShippingFinalDestination:1,ShippingId:1,ShippingInVoiceDate:1,ShippingInVoiceNo:1,ShippingMeasure:1,ShippingOrderNo:1,ShippingPackingSlipNo:1,ShippingPaymentTerms:1,ShippingTotalAmount:1,ShippingTotalBox:1,ShippingTotalWeight:1,ShippingVesselOrFlightNo:1,TotalRate:1,Traiff:1,UnitRate:1,Uom:1}},_Ext:{AuditLog:{ActionDate:1,ActionType:1,Changes:1,EntityId:1,EntityTableName:1,FeatureName:1,FormattedActionDate:1,Id:1,IpAddress:1,SessionId:1,UserId:1}}},Forms:{Membership:{ChangePassword:{FormTitle:1,SubmitButton:1,Success:1},ForgotPassword:{BackToLogin:1,FormInfo:1,FormTitle:1,SubmitButton:1,Success:1},Login:{FacebookButton:1,ForgotPassword:1,GoogleButton:1,LogInButton:1,LoginToYourAccount:1,OR:1,RememberMe:1,SignInButton:1,SignUpButton:1},ResetPassword:{BackToLogin:1,EmailSubject:1,FormTitle:1,SubmitButton:1,Success:1},SignUp:{AcceptTerms:1,ActivateEmailSubject:1,ActivationCompleteMessage:1,BackToLogin:1,ConfirmEmail:1,ConfirmPassword:1,DisplayName:1,Email:1,FormInfo:1,FormTitle:1,Password:1,SubmitButton:1,Success:1}}},Navigation:{LogoutLink:1,SiteTitle:1},Site:{AccessDenied:{ClickToChangeUser:1,ClickToLogin:1,LackPermissions:1,NotLoggedIn:1,PageTitle:1},BasicProgressDialog:{CancelTitle:1,PleaseWait:1},BulkServiceAction:{AllHadErrorsFormat:1,AllSuccessFormat:1,ConfirmationFormat:1,ErrorCount:1,NothingToProcess:1,SomeHadErrorsFormat:1,SuccessCount:1},Dashboard:{ContentDescription:1},Layout:{FooterCopyright:1,FooterInfo:1,FooterRights:1,GeneralSettings:1,Language:1,Theme:1,ThemeBlack:1,ThemeBlackLight:1,ThemeBlue:1,ThemeBlueLight:1,ThemeGreen:1,ThemeGreenLight:1,ThemePurple:1,ThemePurpleLight:1,ThemeRed:1,ThemeRedLight:1,ThemeYellow:1,ThemeYellowLight:1},RolePermissionDialog:{DialogTitle:1,EditButton:1,SaveSuccess:1},UserDialog:{EditPermissionsButton:1,EditRolesButton:1},UserPermissionDialog:{DialogTitle:1,Grant:1,Permission:1,Revoke:1,SaveSuccess:1},UserRoleDialog:{DialogTitle:1,SaveSuccess:1},ValidationError:{Title:1}},Validation:{AuthenticationError:1,CantFindUserWithEmail:1,CurrentPasswordMismatch:1,DeleteForeignKeyError:1,EmailConfirm:1,EmailInUse:1,InvalidActivateToken:1,InvalidResetToken:1,MinRequiredPasswordLength:1,SavePrimaryKeyError:1,UsernameAndPasswordRequired:1}});
}
