(function(){
	$.fn.extend ({
		jFormUtils:{
			opts:{},
			isInputBoxEmpty:function(inputBox){
				if((inputBox.val()=='') && inputBox.attr('jformNotAllowNull')=="true"){//注意短路
					inputBox.addClass("error").after($('<span class="hint">不能为空</span>'));
					return true;
				}
				return false;
			},
			isGroupEmpty:function(group,type){
				if(group.attr('jformNotAllowNull')=='true'){
					var elems = group.find('input[type="'+type+'"]');
					var allElementsEmpty = true;
					$.each(elems,function(i,v){
						if($(v).prop("checked")){
							allElementsEmpty = false;
							return false;
						}
					});
					if(allElementsEmpty){
						group.append($('<span class="hint">至少选一项</span>'));
					}
					return allElementsEmpty;
				}
				return false;
			},
			notMatchType:function(inputBox){
				var regexes = {
					number:{regex:/^([+-]?)\d*\.?\d+$/,errorInfo:"不是数字"},
					username:{regex:/^[A-Za-z0-9_\-\u4e00-\u9fa5]+$/,errorInfo:"不符合用户名格式"},
					mobile:{regex:/0?(13|14|15|18)[0-9]{9}/,errorInfo:"不是中国的手机号码"},
					email:{regex:/\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+/,errorInfo:"错误的邮件地址"},
					id:{regex:/\d{17}[\d|x]|\d{15}/,errorInfo:"非法身份证号格式"},
					integer:{regex:/^-?[1-9]\d*$/,errorInfo:"不是整数"},
					positiveNumber:{regex:/^[1-9]\d*|0$/,errorInfo:"不是正数"},
					nagtiveNumber:{regex:/^-[1-9]\d*|0$/,errorInfo:"不是负数"},
					ascii:{regex:/^[\x00-\xFF]+$/,errorInfo:"不是ASCII码"},
					letter: { regex: /^[A-Za-z]+$/, errorInfo: "不是一个单词" },
					postalcode: { regex: /\d{6}/ , errorInfo: "不是邮政编码" },
					homephone: { regex: /[0-9-()（）]{7,18}/ , errorInfo: "格式错误" },
					realname: { regex: /^[A-Za-z0-9\u4e00-\u9fa5]+$/, errorInfo: "格式错误" },
					password: { regex: /^[A-Za-z0-9_-]+$/, errorInfo: "密码由数字，大小写字母，下划线组成" }
					//etc
				};

				if(inputBox.attr('jformtype')!=undefined&&(inputBox.attr('jformNotAllowNull')=="true"&&inputBox.val()==''||inputBox.val()!='')){
					var regex = regexes[inputBox.attr("jformtype")].regex;
					if(!regex.test(inputBox.val())){
						inputBox.addClass("error").after($('<span class="hint">'+regexes[inputBox.attr("jformtype")].errorInfo+'</span>'));
						return true;
					}
				}
				return false;
			},
			notMatchCostomRegex:function(){
				var config = this.opts.config;
				var hasNotMatchItem = false;
				$.each(config,function(i,v){
					var inputbox = $('#'+v.inputboxId);
					if(inputbox.attr('jformNotAllowNull')=="true"||inputbox.val()!=''){
						if( !v.regex.test(inputbox.val())){
							inputbox.addClass("error").after($('<span class="hint">'+v.errorInfo+'</span>'));
							hasNotMatchItem = true;
						}
					}
				});
				return hasNotMatchItem;
			}
		},
		jFormCheck:function(elem){
			return function(){
				var form = $(elem);
				var inputBoxes = form.find('input[type="text"],input[type="password"],textarea');
				//清空上次错误提示
				form.attr("isok","true");
				form.find('span.hint').remove();
				inputBoxes.removeClass("error");
				//处理输入框
				$.each(inputBoxes,function(i,v){
				//判空、判断是否符合已有类型、判断是否符合自定义正则表达式
					if(elem.jFormUtils.isInputBoxEmpty($(v))||elem.jFormUtils.notMatchType($(v))){
						form.attr("isok","false");
					}
				});

				//处理checkbox组合radio组 //只需要判空
				var radiogroups = form.find('.radiogroup');
				$.each(radiogroups,function(i,v){
					if(elem.jFormUtils.isGroupEmpty($(v),"radio")){
						form.attr("isok","false");
					}
				});
				var checkboxgroups = form.find('.checkboxgroup');
				$.each(checkboxgroups,function(i,v){
					if(elem.jFormUtils.isGroupEmpty($(v),"checkbox")){
						form.attr("isok","false");
					}
				});

				elem.jFormUtils.notMatchCostomRegex();
				if(form.attr("isok")=="true"){
					return true;
				}else{
					return false;
				}
			};
		},
		jFormIsOk:function(){
			return this.jFormCheck(this)();
		},
		jFormInit:function(options){
			var defaults = {
				triggerTargetId:"",//目标 默认值是什么比较好？
				triggerEvent:"click",//触发事件类型 和jQuery的事件类型一样
				config:[]//特殊的正则表达式
			};
			this.jFormUtils.opts = $.extend(defaults,options);
			$('#'+this.jFormUtils.opts.triggerTargetId).bind(this.jFormUtils.opts.triggerEvent,this.jFormCheck(this));
			//紧接着还要绑定各种keyup/change事件，调用jFormUtils
		/*	var inputboxes = this.find('input[type="text"],input[type="password"],textarea');
			var utils = this.jFormUtils;
			var handler = function(e){
				return function(){
					utils.isInputBoxEmpty(e)
					utils.notMatchType(e);
				}
			};
			$.each(inputboxes,function(i,v){
				$(v).bind("keyup",handler($(v)));
			});*/
		}
	});
})(jQuery);