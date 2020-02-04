<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accessDenied.aspx.cs" Inherits="accessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .fa-ban {
  color: red;
}
        .fa-lg{
                font-size: 12em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper">
        <section class="content-header">
            <h1>Access Denied  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a  href="accessDenied.aspx">Access Denied  </a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
           
     
        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
           
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">Access Denied  </h3>
                            
                          </div>
                    <div class="box-body table-responsive" style="text-align:center;height:200px;vertical-align:center">
                      
                     
                        <i class="fa fa-ban fa-lg" aria-hidden="true" ></i>
                              
                       
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
        
                            </asp:UpdatePanel>
    </div>
</asp:Content>

