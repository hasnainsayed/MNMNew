<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bulkSalesMultiplLocaExcel.aspx.cs" Inherits="bulkSalesMultiplLocaExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Bulk Sales Multiple Location - Excel
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Bulk Uploads</li><li>Bulk Sales</li>
            <li class="active">Bulk Sales Multiple Location - Excel</li>
        </ol>
    </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                    <section>
                                           <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Bulk Sales Multiple Location - Excel</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
   
                         <div class="row">
                             <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                             <div id="divInsert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                             <div id="divUpdate" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                           <table class="table table-bordered">
                               <tr>
                                  
                                   <td>
                                       <asp:FileUpload ID="FileUpload1" runat="server" />
                                   </td>
                                   <td>
                                       <asp:LinkButton ID="uploadExcel" runat="server" CssClass="btn btn-sm btn-warning" OnClick="uploadExcel_Click" OnClientClick="return confirm('Do you Want to Upload ?');"><i class="fa fa-search"></i> Upload</asp:LinkButton>
                                   </td>
                                   
                               </tr>
                               
                           </table>
                            </div>
                        </div>
                    </div>
                </div>
                                               </div>
                                        
                                    </section>
         </ContentTemplate>  
            <Triggers>
				<asp:PostBackTrigger ControlID="uploadExcel" />
			</Triggers>
        </asp:UpdatePanel>
                                 
        </div>
</asp:Content>

