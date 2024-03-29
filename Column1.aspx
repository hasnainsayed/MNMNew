﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Column1.aspx.cs" Inherits="Column1aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>
                <asp:Label ID="lblCol" runat="server" Text=""></asp:Label>
            </h1>
            <ol class="breadcrumb">
                <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Style Category</li>
                <li class="active">
                    <asp:Label ID="lblCol1" runat="server" Text=""></asp:Label></li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <!-- Form  -->
                <div class="box box-primary" id="ctfrmDet" runat="server" visible="false">
                    <div class="box-body">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblColName" runat="server" Text=""></asp:Label></label>
                                <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="reqname" ErrorMessage="Please enter name" ControlToValidate="txtName" Display="Dynamic" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                         <div class="col-md-6">
                                <div class="form-group">
                                    <label>Brand Image (25KB,H:300px,W:300px)</label>
                                    <asp:RegularExpressionValidator ID="revImage" ValidationGroup="save" ControlToValidate="FileUpload1" forecolor="Red" ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$" Text="Only .jpg,.png,.jpeg,.gif Files are allowed" runat="server" />
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><br/>
                                    <asp:Image ID="image1Display" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                </div>
                            </div>
                        <div class="col-md-6">
                                <div class="form-group">
                                    <label>Brand Status</label>
                                    <asp:DropDownList ID="brandStatus" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="1">Show</asp:ListItem>
                                        <asp:ListItem Value="0">Hide</asp:ListItem>                                       
                                    </asp:DropDownList>
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
                                    <asp:Label ID="lblErrorMsg" runat="server" Visible="false" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnCount" runat="server" />
                                    <h3 class="box-title" style="float: right">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnAdd_Click" /></h3>
                                    <div class="box-body table-responsive " style="border: none !important">
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
                                    <table id="" class="table table-bordered table-striped dtSearch">
                                        <thead>
                                            <tr>
                                                
                                                <asp:Label ID="hdnID" runat="server" Text=""></asp:Label>
                                                <th>ID</th>
                                                <th>
                                                    <asp:Label ID="lblColName1" runat="server" Text=""></asp:Label></th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="GV" runat="server" OnItemCommand="rptr_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("Col1ID")%> </td>
                                                        <td><%# Eval("C1Name")%> </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="edit"
                                                                ImageUrl="images/edit.png" CommandArgument='<%# Eval("Col1ID") %>' />
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
             <Triggers>
				<asp:PostBackTrigger ControlID="btnSave" />
                 <asp:PostBackTrigger ControlID="btnUpdate" />
                 
			</Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

