<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js" type="text/jscript"></script>

    <link rel="shortcut icon" href="favicon.ico" >
    <link href="estilo/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
    	<div class="row">
			<div class="col-md-6 col-md-offset-3">
				<div class="panel panel-login">
					<div class="panel-heading">
						<div class="row">
							<div class="col-xs-12 text-center">
								<img src="Imagenes/logoQnt.png" alt="" />
							</div>
						</div>
						<hr />
					</div>
					<div class="panel-body">
						<div class="row">
							<div class="col-lg-12">
								<div class="form-group">
									<asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="*" ControlToValidate="txtUsuario"></asp:RequiredFieldValidator>
								</div>
								<div class="form-group">
									<asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control"  placeholder="Contraseña"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfvClave" runat="server" ErrorMessage="*" ControlToValidate="txtClave"></asp:RequiredFieldValidator>
								</div>
								<div class="form-group">
									<div class="row">
										<div class="col-sm-6 col-sm-offset-3">
											<asp:button id="btnIngresar" CssClass="btn btn-primary" Width="100%" runat="server" Text="Ingresar" CausesValidation="true" OnClick="btnIngresar_Click"></asp:button>
											<asp:Label ID="lblMensaje" runat="server"></asp:Label>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
    </form>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/jscript"></script>
</body>
</html>
