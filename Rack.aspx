<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rack.aspx.cs" Inherits="Rack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hdnSubloactionID" runat="server" />
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Rack
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Rack</li>

            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <!-- Form  -->
                <div class="box box-primary" id="ctfrmDet" runat="server" visible="false">

                    <div class="box-body">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Rack</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="reqname" ErrorMessage="Please enter name" ControlToValidate="txtName" Display="Dynamic" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                       <asp:Button ID="btnSave" Visible="false" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" Visible="false" class="btn btn-warning pull-right" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" class="btn btn-danger pull-right" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                    </div>
                </div>

                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">

                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>
                                         <asp:Button ID="btnBack" runat="server" Text="Back" class="btn btn-primary" CausesValidation="False" OnClick="btnBack_Click" /></h3>
                                    <div class="box-body table-responsive " style="border:none !important">
                                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Added</h5>
                                        </div>

                                        <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                            <h5><i class="icon fa fa-check"></i>Succesfully Updated</h5>
                                        </div>

                                    </div>
                                </div>
                                <div class="box-body">
                                    <table id="example111" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                <asp:HiddenField ID="hdnID" runat="server" />
                                                <th>ID</th>
                                                <th>Rack</th>
                                                 <th>Add</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("RackID")%> </td>
                                                        <td><%# Eval("Rack")%> </td>
                                                          <td>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="addstack" CausesValidation="False" class="btn btn-primary" CommandArgument='<%# Eval("RackID") %>'
                                                                runat="server"><i class="icon-remove"></i>Stack</asp:LinkButton></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("RackID") %>' />

                                                            <%--<asp:ImageButton ID="ImageButton2" runat="server" CommandName="addstack"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("RackID") %>' />--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

