package lvb.sdk;


import java.security.MessageDigest;  
  
/** 
 * 采用MD5加密解密 
 * @author 辜庆渝 
 * @datetime 2017-03-06 
 */  
public class MD5Util {  
  
    /*** 
     * MD5加码 生成32位md5码 
     */  
    public static String string2MD5(String inStr){  
    	 try {
             byte[] btInput = inStr.getBytes();
             MessageDigest mdInst = MessageDigest.getInstance("MD5");
             mdInst.update(btInput);
             byte[] md = mdInst.digest();
             StringBuffer sb = new StringBuffer();
             for (int i = 0; i < md.length; i++) {
                 int val = ((int) md[i]) & 0xff;
                 if (val < 16)
                     sb.append("0");
                 sb.append(Integer.toHexString(val));

             }
             return sb.toString();
         } catch (Exception e) {
             return null;
         }
  
    }  
  
    public static String string2MD5(String inStr,String charset){  
    	try {
            byte[] btInput = inStr.getBytes(charset);
            MessageDigest mdInst = MessageDigest.getInstance("MD5");
            mdInst.update(btInput);
            byte[] md = mdInst.digest();
            StringBuffer sb = new StringBuffer();
            for (int i = 0; i < md.length; i++) {
                int val = ((int) md[i]) & 0xff;
                if (val < 16){
                	sb.append("0");
                }
                sb.append(Integer.toHexString(val));
            }
            return sb.toString();
        } catch (Exception e) {
            return null;
        }
  
    }  
    

}  