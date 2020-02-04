<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IssueBag.aspx.cs" Inherits="IssueBag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Add Issue Bag
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Master</li>
                <li class="active">Add Issue Bag</li>

            </ol>
        </section>

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <asp:Label ID="hdnID" runat="server" Visible="false" />
                <!-- Form  -->
                <div class="box box-primary" id="ctfrmDet" runat="server" visible="false">

                    <div class="box-body">
                        <div class="col-md-6">
                           <div class="form-group">
                                <label>Purchase Indent</label>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlLocationType" ID="selectproj"
                                    ValidationGroup="grp" ErrorMessage="Please select project" ForeColor="Red"
                                    InitialValue="0" runat="server"  Display="Dynamic"/>

                                    <asp:DropDownList ID="ddlLocationType" runat="server" DataTextField="name" DataValueField="id" CssClass="form-control">
                                        <asp:ListItem Text="---- Select ----" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                            </div>
                        </div>

                         <div class="col-md-6">
                            <div class="form-group">
                                <label>No. of Bags</label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="reqno" ErrorMessage="Please enter name" ControlToValidate="txtHSN" Display="Dynamic" />
                                <asp:TextBox ID="txtHSN" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                      
                    </div>
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                        <asp:Button ID="btnSave" class="btn btn-success pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
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
                                                <th>ID</th>
                                                <th>Item Category</th>
                                                 <th>HSN Code</th>
                                                <th>Tax %</th>
                                               
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="GV" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("id")%> </td>
                                                        <td><%# Eval("name")%> </td>
                                                        <td><%# Eval("Description")%> </td>
                                                        <td><%# Eval("date")%> </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("id") %>' />
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

