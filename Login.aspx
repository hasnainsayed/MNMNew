<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css" />

    <link href="Validation/css/jqueryui.css" rel="stylesheet" type="text/css" />
    <link href="Validation/css/Validation.css" rel="stylesheet" type="text/css" />
    <link href="datepicker/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <style>
        body {
            background-image: url('../images/bg.png') !important;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
        <div class="login-box">
            <div class="login-logo">
                <b>
                    <%--<img src="images/logo-1.png" />--%>MNM</b>
            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">
                <p class="login-box-msg">Sign in to start your session</p>

                <span style="text-align: center !important; padding: 10px;">
                    <asp:Label ID="lblErrorMsg" Font-Bold="true" ForeColor="Red" Visible="false"
                        Font-Size="15px" runat="server" Text=""></asp:Label></span>

                <div class="form-group has-feedback">
                    <%--<input type="email" class="form-control" placeholder="Email">--%>
                    <asp:TextBox ID="txtUserName" class="form-control validate[required]" placeholder="User Name" runat="server"></asp:TextBox>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control validate[required]" placeholder="Password" runat="server"></asp:TextBox>
                    <%-- <input type="password" class="form-control" placeholder="Password">--%>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">

                    <!-- /.col -->
                    <div class="col-xs-12">
                        <%-- <button type="submit" class="btn btn-primary btn-block btn-flat">Sign In</button>--%>
                        <asp:Button ID="btnLogin" class="btn btn-primary btn-block btn-flat" runat="server" Text="Sign In"
                            OnClick="btnLogin_Click" />
                    </div>
                    <!-- /.col -->
                </div>

            </div>
            <!-- /.login-box-body -->
        </div>
    </form>
    <!-- /.login-box -->

    <!-- jQuery 3 -->

    <script src="Validation/js/jquery_Master_1.9.1.js" type="text/javascript"></script>
    <script src="Validation/js/jquery_UI_1.10.3.js" type="text/javascript"></script>
    <script src="Validation/js/jquery.min.js" type="text/javascript"></script>


    <script type="text/javascript" src="../../bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script type="text/javascript" src="../../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script type="text/javascript" src="../../plugins/iCheck/icheck.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' /* optional */
            });
        });
    </script>
</body>
</html>
