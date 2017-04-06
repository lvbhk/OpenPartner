package lvb.sdk;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.nio.charset.Charset;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.http.HttpEntity;
import org.apache.http.HttpStatus;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.util.EntityUtils;

import com.google.gson.Gson;




public class HttpClintUtil {
	private static Log log = LogFactory.getLog(HttpClintUtil.class);
	private static  boolean DoPost(String callbackUrl,String json){
		// 创建默认的httpClient实例.  
        CloseableHttpClient httpclient = HttpClients.createDefault(); 
        // 创建httppost  
        HttpPost httppost = new HttpPost(callbackUrl);
        try { 
        	httppost.setEntity(new StringEntity(json, Charset.forName("UTF-8")));
        	httppost.setHeader("Accept", "application/json");
        	httppost.setHeader("Content-type", "application/json");
        	System.out.println("executing request " + httppost.getURI()); 
          CloseableHttpResponse response = httpclient.execute(httppost); 
          try { 
            HttpEntity entity = response.getEntity(); 
            
            if (entity != null) { 
              System.out.println("--------------------------------------"); 
              System.out.println("Response content: " + EntityUtils.toString(entity, "UTF-8")); 
              System.out.println("--------------------------------------"); 
            } 
	      	  if(response.getStatusLine().getStatusCode() == HttpStatus.SC_OK){
	    		  return true;
	    	  }
          } finally { 
            response.close(); 
          } 
        } catch (ClientProtocolException e) { 
        	log.error(e);
        } catch (UnsupportedEncodingException e) { 
        	log.error(e);
        } catch (IOException e) { 
        	log.error(e);
        } finally { 
          // 关闭连接,释放资源  
          try { 
            httpclient.close(); 
          } catch (IOException e) { 
        	  log.error(e);
          } 
        }
		return false; 
	}
	
	public static boolean Delivery(DeliveryFilterModel model,String token){
		Gson gson = new Gson();
		 String md5str = model.getPartnerId() + model.getWarehouse() + model.getExpressType() + model.getPackageId() + model.getOutPackageId() + model.isIsUPU() + model.getDeliveryTime() + model.getUserName() + token;
	        String postUrl = "http://api.lvb.com:8080/delivery?Sing=" + MD5Util.string2MD5(md5str, "utf-8");
	        boolean isTrue = DoPost(postUrl,gson.toJson(model));
	        System.out.println(isTrue);
	        if (!isTrue) {
	        	return false;
			}
		return true;
	}
}
