
# Core.SimpleTemp
#我是抓取开始标记#
Demo地址：http://132.232.18.92:8080
<br>  
主题：简单，实用、高效，基于NetCore官方

目前进行中：列表页更换为bootstrap-table、优化已有功能。JwtBearer完善
近 期 计划：写几个页面Demo
后 期 计划：页面批量生成。

特点：自动生成查询表达式树,同时支持两种认证(CookieAuthentication/Api JwtBearer)、 精确的授权控制、自动DI、全局异常处理、分布式内存缓存、静态文件缓存、以及基于队列的文件日志、同时支持多数据库 ,可采用不同类型数据库、（目前演示Demo使用-PostgreSQL， 本地开发使用-Sql Server）
DB导航属性自动Include、


//Demo集成部署/环境<br>
系统环境：Centos7.5 ; Cpu/1h ; 内存/1g  网络/1M （其实就是腾讯云10元/月）<br>
数 据 库：PostgreSQL 9.5.15<br>
持续集成：Jenkins  <br>
进程守护：Supervisor<br>
<br>
//前端<br>
页面样式：bootstrap3<br>
页面排版：AdminLte2<br>
弹窗：Layer<br>
select多选：select2<br>
Tree：JsTree<br>
bootstrap-Table<br>
<br>
//后端<br>
后端完全遵循AspNetCore标准开发<br>
认证：web 基于CookieAuthentication \Api 已支持JwtBearer<br>
授权：自定义AuthorizationHandler+Filter<br>
缓存：Distributed （目测服务器资源有限采用DistributedMemoryCache）<br>
日志：采用基于队列的FileLog<br>
数据访问：EFcore<br>




#我是抓取标记结束#