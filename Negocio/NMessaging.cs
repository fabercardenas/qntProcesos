using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio
{
    public static class NMessaging
    {
        public static string Error(Exception ex)
        {
            return Error(ex.Message);
        }

        public static string Error(string msg)
        {

            return "<div class='alert alert-danger'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" + msg + "</div>";
        }

        public static string Info(string msg)
        {
            return "<div class='alert alert-info'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" + msg + "</div>";
        }

        public static string Success(string msg)
        {

            return "<div class='alert alert-success'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" + msg + "</div>";
        }

        public static string Warning(string msg)
        {

            return "<div class='alert alert-warning'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" + msg + "</div>";
        }


        #region Messaging Generic

        public static string GenericInsert()
        {
            return "<div id='success' class='alert alert-success'>Registro(s) ingresado(s) con exito!</div>";
        }

        public static string GenericUpdate()
        {
            return "<div id='success' class='alert alert-success'>Registro(s) actualizado(s) con exito</div>";
        }

        public static string GenericDelete()
        {
            return "<div id='success' class='alert alert-success'>Registro(s) eliminado(s) con exito</div>";
        }

        #endregion
    } 
}