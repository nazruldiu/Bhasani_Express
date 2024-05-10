namespace BMS_Scheduler.Membership {

    @Serenity.Decorators.registerClass()
    export class LoginPanel extends Serenity.PropertyPanel<LoginRequest, any> {
        protected getTemplateName() { return 'LoginPanel'; }
        protected getFormKey() { return LoginForm.formKey; }

        constructor(container: JQuery) {
            super(container);

            this.byId('LoginButton').click(e => {
                e.preventDefault();

                if (!this.validateForm()) {
                    return;
                }

                var request = this.getSaveEntity();

                //var _UserName = $('#Username').val();
                //var _Password = $('#Password').val();
                //if (_UserName == "" || _Password == "") {
                //    Q.notifyError(Q.text("Validation.UsernameAndPasswordRequired"));
                //    return;
                //}
                //request.Username = _UserName;
                //request.Password = _Password;

                $('#LoginButton').prop('disabled', true);
                Q.serviceCall({
                    url: Q.resolveUrl('~/Account/Login'),
                    request: request,
                    onSuccess: response => {
                        $('#LoginButton').prop('disabled', false);
                        this.redirectToReturnUrl();
                    },
                    onError: (response: Serenity.ServiceResponse) => {


                        if (response != null && response.Error != null && !Q.isEmptyOrNull(response.Error.Message)) {
                            Q.notifyError(response.Error.Message);
                            $('#Password').focus();

                            return;
                        }

                        Q.ErrorHandling.showServiceError(response.Error);
                    }
                });
            });
        }

        protected redirectToReturnUrl() {
            var q = Q.parseQueryString();
            var returnUrl = q['returnUrl'] || q['ReturnUrl'];
            if (returnUrl) {
                var hash = window.location.hash;
                if (hash != null && hash != '#')
                    returnUrl += hash;
                window.location.href = returnUrl;
            }
            else {
                //window.location.href = Q.resolveUrl('~/');
                window.location.href = Q.resolveUrl('~/Dashboard/Index');
            }
        }

        protected getTemplate() {
            return `
    <div class="s-Panel p-4">
    <h2 class="text-center p-4">
        <img src="${Q.resolveUrl("~/Content/site/images/serenity-logo-w-128.png")}"
            class="rounded-circle p-1" style="background-color: var(--s-sidebar-band-bg)"
            width="50" height="50" /> Bhasani
    </h2>

        <form id="~_Form" action="">
            <div id="~_PropertyGrid"></div>
            <div class="px-field" style="text-align:center;">
                <button id="~_LoginButton" type="submit" class="btn btn-primary my-3 w-100" style="max-width:200px;margin-left:20px;">
                    ${Q.text("Forms.Membership.Login.LogInButton")}
                </button>
            </div>
             <div class="px-field">
                <a class="text-decoration-none" href="${Q.resolveUrl('~/Account/ForgotPassword')}">
                    ${Q.text("Forms.Membership.Login.ForgotPassword")}
                </a>
            </div>
        </form>
    </div>


`;
        }
    }
}
