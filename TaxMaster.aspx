<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="TaxMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper">
    <section class="content-header">
        <h1>Tax Master
        <!--<small>Control panel</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Masters</li>
            <li class="active">Tax Master</li>
        </ol>
    </section>
       <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
    <div class="row" runat="server" id="divAgCust">
        <div class="col-xs-12">
            <div class="box box-primary">
                <div class="box-header ">
                    <h3 class="box-title">Tax</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                    </div>
                </div>

                <div class="box-body table-responsive ">
                    <asp:Label ID="lblid" runat="server" Text="" Visible="false"></asp:Label>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Tax %</label>
                            <asp:TextBox ID="txttax" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->
                    <!-- /.col -->

                    <div class="col-md-12">

                        <div class="form-group">
                            <asp:Button ID="btnSave" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->

                </div>

            </div>


        </div>

    </div>
                                    </ContentTemplate>
           </asp:UpdatePanel>
         </div>

</asp:Content>

