<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="smsService.aspx.cs" Inherits="smsService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper">
        <section class="content-header">
            <h1>SMS Service</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Communication Management</li>
                <li class="active"><a  href="smsService.aspx">SMS Service</a></li>
               
            </ol>
        </section>
   
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            
                
                <div class="box">
                    <div class="box-header">
                       
                          </div>
                    <div class="box-body table-responsive">
                        <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Updated Succesfully</h5>

                                    </div>

                                    <div id="divUpdAlert" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Update Failed</h5>

                                    </div>
                      <table class="table table-bordered table-striped">
                            <tr>
                                <th width="10%">Message</th>
                                <td>
                                    
                                    <textarea id="smsMessage" cols="20" rows="4" runat="server" class="form-control"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <asp:Label ID="smsid" runat="server" Text="" Visible="false"></asp:Label>
                                <th>API Key</th>
                                <td><asp:TextBox ID="apikey" runat="server" class="form-control"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <th>Sender</th>
                                 <td><asp:TextBox ID="smsSender" runat="server" class="form-control"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <th>Log</th>
                                  <td>
                                    <textarea id="logs" readonly="readonly" cols="20" rows="2" runat="server" class="form-control"></textarea>
                                </td>
                            </tr>
                          <tr>
                              <td colspan="2">  <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save SMS Details" OnClick="btnSave_Click" />
                        
                    </div></td>
                          </tr>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
   
            </div>
</asp:Content>

