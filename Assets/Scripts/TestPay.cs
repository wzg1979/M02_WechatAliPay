using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cn.paysdk.unity;

public class TestPay : MonoBehaviour, PaySDKHandler {
    Text txtResult;

    private void Awake() {
        //注册按钮事件
        GameObject.Find("WechatPayBtn").GetComponent<Button>().onClick.AddListener(delegate () { OnWechatPayBtn(); });
        GameObject.Find("AliPayBtn").GetComponent<Button>().onClick.AddListener(delegate () { OnAliPayBtn(); });
        txtResult = GameObject.Find("OnResultTxt").GetComponent<Text>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ShowResult(string result) {
        txtResult.text += result;
    }
    /// <summary>
    /// 微信支付
    /// </summary>
    void OnWechatPayBtn() {
        txtResult.text = "开始微信支付";

        PayItem(PaySDKChannel.PaySDKChannelWechat);
    }
    void OnAliPayBtn() {
        txtResult.text = "开始支付宝支付";
        PayItem(PaySDKChannel.PaySDKChannelAlipay);
    }

    /// <summary>
    /// 支付功能函数
    /// </summary>
    /// <param name="channel"></param>
    void PayItem(PaySDKChannel channel) {
        //创建订单实例
        PaySDKOrder order = new PaySDKOrder();
        order.orderId = "订单ID";
        order.amount = 1;//支付金额;  单位是分钱
        order.subject = "支付subject";
        order.body = "支付主体";
        order.des = "支付描述信息";
        order.metadata = "元数据";

        //发起支付
        PaySDK.payWithOrder(order, channel, this);
    }

    #region 支付接口回调函数
    public bool onWillPay(string ticketId) {
        //throw new System.NotImplementedException();
        return false;
    }

    public void onPayEnd(PaySDKStatus status, string ticketId, long errorCode, string errorDes) {
        //throw new System.NotImplementedException();
        //Debug.Log("Status:" + status + "  ticketId:" + ticketId + "  errorCode:" + errorCode + "errorDes:" + errorDes);
        ShowResult("Status:" + status + "  ticketId:" + ticketId + "  errorCode:" + errorCode + "errorDes:" + errorDes);
        if (status == PaySDKStatus.PaySDKStatusCancel) {
            //result = "Pay Cancel";
            ShowResult("Pay Cancel");
        } else if (status == PaySDKStatus.PaySDKStatusSuccess) {
            //result = "Pay Success";
            ShowResult("Pay Success");
            //添加道具。。。生效

        } else if (status == PaySDKStatus.PaySDKStatusFail) {
            //result = "Pay Fail Error:" + errorCode + "  Des:" + errorDes;
            ShowResult("Pay Fail Error:" + errorCode + "  Des:" + errorDes);
        } else {
            //result = "Pay Result Unknown  Error:" + errorCode + "  Des:" + errorDes;
            ShowResult("Pay Result Unknown  Error:" + errorCode + "  Des:" + errorDes);
        }
    }
    #endregion
}
