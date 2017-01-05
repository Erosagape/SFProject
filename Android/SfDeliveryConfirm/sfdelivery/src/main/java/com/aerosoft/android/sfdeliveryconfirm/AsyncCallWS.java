package com.aerosoft.android.sfdeliveryconfirm;

import android.os.AsyncTask;
import android.widget.TextView;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.PropertyInfo;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

/**
 * Created by Puttipong on 13/02/2559.
 */
public class AsyncCallWS extends AsyncTask<String, Void, Void> {
    private static String NAMESPACE = "http://tempuri.org/";
    private static String URL = "http://tracking.sfaerosoft.com/IService.svc?wsdl";
    private static String SOAP_ACTION = "http://tempuri.org/IDataExchange/";
    private String RunProcess;
    private String param1;
    private String param2;
    private String resultText;
    boolean success;
    private ClsUser user;
    private TextView tv;
    public static String UpdateDeliveryWS(String docno,String cmdSet)
    {
        String resTxt = null;
        String webMethod="UpdateDeliveryInfo";
        SoapObject req=new SoapObject(NAMESPACE,webMethod);

        PropertyInfo pDocNo=new PropertyInfo();
        pDocNo.setName("docno");
        pDocNo.setValue(docno);
        pDocNo.setType(String.class);
        req.addProperty(pDocNo);

        PropertyInfo pCmd=new PropertyInfo();
        pCmd.setName("cmdset");
        pCmd.setValue(cmdSet);
        pCmd.setType(String.class);
        req.addProperty(pCmd);

        SoapSerializationEnvelope env=new SoapSerializationEnvelope(SoapEnvelope.VER11);
        env.dotNet=true;
        env.setOutputSoapObject(req);
        HttpTransportSE ws=new HttpTransportSE(URL);
        try
        {
            ws.call(SOAP_ACTION+webMethod,env);
            SoapPrimitive resp=(SoapPrimitive)env.getResponse();
            resTxt=resp.toString();
        }
        catch (Exception e)
        {
            resTxt=e.getMessage();
        }
        return resTxt;
    }
    public static String GetDeliveryWS(String docno)
    {
        String resTxt = null;
        String webMethod="GetDeliveryInfo";
        SoapObject request = new SoapObject(NAMESPACE, webMethod);

        PropertyInfo sayShowData = new PropertyInfo();
        sayShowData.setName("docno");
        sayShowData.setValue(docno);
        sayShowData.setType(String.class);
        request.addProperty(sayShowData);

        PropertyInfo sayCliteria = new PropertyInfo();
        sayCliteria.setName("doctype");
        sayCliteria.setValue("2");
        sayCliteria.setType(String.class);
        request.addProperty(sayCliteria);
        // Create envelope
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
                SoapEnvelope.VER11);
        envelope.dotNet = true;
        envelope.setOutputSoapObject(request);
        HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);

        try {
            androidHttpTransport.call(SOAP_ACTION+webMethod, envelope);
            SoapPrimitive response = (SoapPrimitive) envelope.getResponse();
            resTxt = response.toString();
        } catch (Exception e) {
            resTxt = e.getMessage();
        }
        return resTxt;
    }
    public static String LoginWS(String user, String password) {
        String resTxt = null;
        String webMethod="Login";
        SoapObject request = new SoapObject(NAMESPACE, webMethod);

        PropertyInfo sayShowData = new PropertyInfo();
        sayShowData.setName("user");
        sayShowData.setValue(user);
        sayShowData.setType(String.class);
        request.addProperty(sayShowData);

        PropertyInfo sayCliteria = new PropertyInfo();
        sayCliteria.setName("password");
        sayCliteria.setValue(password);
        sayCliteria.setType(String.class);
        request.addProperty(sayCliteria);
        // Create envelope
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(
                SoapEnvelope.VER11);
        envelope.dotNet = true;
        envelope.setOutputSoapObject(request);
        HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);

        try {
            androidHttpTransport.call(SOAP_ACTION+webMethod, envelope);
            SoapPrimitive response = (SoapPrimitive) envelope.getResponse();
            resTxt = response.toString();
        } catch (Exception e) {
            resTxt = e.getMessage();
        }
        return resTxt;
    }
    public void SetParameter(ClsUser usr)
    {
        user=usr;
    }
    public void SetParameter(String procid,String procParam1,String procParam2,TextView txtResult)
    {
        RunProcess=procid;
        param1=procParam1;
        param2=procParam2;
        tv=txtResult;
    }
    public String Result()
    {
        success=false;
        String msg=resultText;
        switch (RunProcess.toUpperCase())
        {
            case "LOGIN":
                if(resultText.substring(0,1).equalsIgnoreCase("W")==true)
                {
                    success=true;
                }
                break;
            case "GET_DELIVERY":
                if(resultText.substring(0,1).equalsIgnoreCase("E")==false)
                {
                    success=true;
                }
                break;
            case "UPDATE_DELIVERY":
                if(resultText.substring(0,1).equalsIgnoreCase("E")==false)
                {
                    success=true;
                }
        }
        return msg;
    }
    protected Void doInBackground(String... params) {
        resultText="";
        switch (RunProcess.toUpperCase())
        {
            case "LOGIN":
                resultText=LoginWS(param1,param2);
                break;
            case "GET_DELIVERY":
                resultText=GetDeliveryWS(param1);
                break;
            case "UPDATE_DELIVERY":
                resultText=UpdateDeliveryWS(param1,param2);
                break;
        }
        return null;
    }
    @Override
    protected void onPreExecute() {
        resultText="";

        success=false;
    }
    @Override
    protected void onPostExecute(Void result) {
        if(tv!=null)
        {
            tv.setText(Result());
        }
        switch (RunProcess.toUpperCase()) {
            case "LOGIN":
                user.setUserPassword(param1,param2);
                user.setUserName(resultText);
                user.setIsLogin(success);
                break;
            case "GET_DELIVERY":
                user.showPopup(resultText);
                break;
            case "UPDATE_DELIVERY":
                user.showPopup(resultText);
                break;
        }
    }
    @Override
    protected void onProgressUpdate(Void... values) {

    }
}
