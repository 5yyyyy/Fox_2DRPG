# Fox_2DRPG
## 该说明仅针对此项目 优化脚本已上传 仅供学习参考思路
### Unity版本 2019.4.32f1c1  
[SerializeField]可以private变量依旧可以在U3D窗口中看到 可以防止在别的脚本中调用函数中调用同名变量  

### 关于地图(未优化碰撞体)  
使用Tilemap(Tile Palette中B为画图快捷键 shift为单个消除快捷键)     
素材Pixels Per Unit(每单位像素数)均为16*16   
地图碰撞体使用Tilemap Collider 2D (优化方法：Tilemap Collider 2D --> 勾选Used By Composite后添加Composite Collider 2D即可将地图单个碰撞体变为整体 需要注意添加Composite Collider 2D后会自动添加Rigidbody 2D需要Rigidbody 2D -->Body Type --> Dynamic 改为 Static 否则地图会掉落)  
单向平台(添加碰撞体后勾选Used By Effector后添加组件Platform Effector 2D后取消勾选Use Collider Mask)  

### 关于地图素材
有一份切割好的素材和一份未切割的素材 (素材切割方法 Sprite Mode-->Single改为Multiple后点击Sprite Editor 进入面板后点击Slice-->Type-->Automatic-->Slice实现自动切割 Revert可以还原切割前 按照单元格尺寸切割在Slice-->Type-->Automatic改为Grid By Cell Size修改参数-->Slice)

### 关于图层
Sorting Layer-->Add Sorting Layer中添加即可(处于下拉栏中越靠下的Layer越会显示在镜头前面 同Sorting Layer时修改Order in Layer中数字 数字越大越显示在镜头前面)

### 关于主角(狐狸)(未优化组件参数手感 移动跳跃代码已优化为NewPlayer.cs)
添加RigidBody 2D(优化方法:RigidBody 2D--> Collision Detection--> Discrete--> Continuous连续检测碰撞优化手感 RigidBody 2D-->Interpolate--> None改为Interpolate 优化人物高空掉落或与墙壁碰撞凹陷等问题)  
本作为2D游戏RigidBody 2D--> Constraints --> Freeze Rotation勾选Z锁定Z轴  
人物碰撞体运用了上BOX Collider 2D下Circle Collider 2D双碰撞体方法  
下蹲(在下蹲后只保留下方Circle Collider 2D 关闭上方BOX Collider 2D 方法为XXX.enabled = false 站立时为true)  
物理2D判定头顶是否有障碍物(使用Physics2D.OverlapCircle(判定点,距离,判定图层)) 

### 关于动画
使用Animation(Window--> Animation--> Animation打开Animation窗口 选择需要添加动画的物品后Animation窗口中Create动画即可)  
更改动画速度方法(拖拽时间轴或在Sample窗口中修改动画采样频率 Sample默认没有在时间轴右上角打开Show Sample Rate)  
动画循环方法(选择创建的动画勾选Loop Time实现循环播放)  
动画通过Animator控制(Window--> Animation--> Animator)  
动画切换方法(在Animator中Make Transition连接动画的切换 Alt可以拖拽整个面板)  
动画切换参数(Animator--> Parameters中添加参数条件实现状态切换)  
UI的渐入(录制动画关键帧Animation中录制改变不透明度)  

### 关于摄像机
添加Cinemachine插件  
使用Cinemachine(Cinemachine--> Create 2D Camera)  
镜头跟随(Follow中拖入跟随的物品)  
镜头锁死区域调整(当跟随物品在锁死区域内镜头不会跟随移动 当跟随物品移出锁死区域则会跟随移动Body--> Dead Zone栏调整)  
摄像机区域限制(为背景添加Polygon Collider 2D设置边缘 按住ctrl可以消除不需要的线 勾选is Trigger 将设置好的背景导入Extension--> Add Extension--> CinemachineConfiner即可)  

### 关于物品收集(未优化人物双碰撞体收集BUG 可以参考DarkRPG中单碰撞体方法)
添加触发区域(添加BOX Collider 2D后勾选is Trigger)  
触发代码(OnTriggerEnter2D通过collision.tag == “xxx”判断 需要为收集物品添加新Tag--> Add Tag 判断后Destroy(collision.gameObject))  

### 关于材质球
为上方BOX Collider 2D添加材质球 防止头部与墙壁摩擦卡住  
材质球制作(Create--> Physics Material 2D 将Friction改为0后添加再在BOX Collider 2D--> Material 此方法BOX Collider 2D判定范围需大于Circle Collider 2D)  
人物和门暗淡材质球(Create--> Material后Shader--> Sprites--> Diffuse)  

### 关于UI
基本背景UI  
添加画布(UI--> Canvas)  
添加文字内容(Canvas下UI--> Text)  
触发代码(需注意使用UI时 命名空间需要引用using UnityEngine.UI)  
固定UI位置(Rect Transform中左上角中可以固定UI位置随画面比例变化而变化)  
门(UI--> Panel 固定门UI位置 用碰撞体触发器触发默认Panel关闭 当获取于主角碰撞体碰撞时XXX.SetActive(true)激活 当人物离开时使用OnTriggerExit2D函数判定人物离开后XXX.SetActive(false) 关闭)  

### 关于敌人
为敌人添加碰撞器(添加RigidBody 2D锁定Z轴 和Circle Collider 2D)  
消灭敌人触发代码(OnCollisionEnter2D通过collision.gameObject.tag == “XXX”判断 需要为敌人添加新Tag--> Add Tag 判断后Destroy(collision.gameObject)  
跳跃消灭敌人(通过主角下落时碰撞敌人作为判定在消灭时做Animation.GetBool(“下落状态”)作为判定条件 下落状态为y值小于0且不与地面接触)  
消灭敌人后反弹跳跃(消灭后添加跳跃代码)  
非跳跃碰撞敌人受伤弹反(在碰撞后判断不是下落状态碰撞后else if(transform.potision < collision.gameObject.transform.position.x)后给向左弹的力 向右同理)  
敌人简易移动AI(在敌人物品下添加两个空子物体放置在敌人物品左右 获取位置后做判定是否越过 越过转身继续运动 如此反复 优化方法:在Start方法中获取两个子物体的值后销毁子物体)  
敌人动画(通过在Animation中IDLE动画中添加Event实现)  
关于引用类(消灭动画挂在敌人物品脚本下 在碰撞判定时通过类名+实体名Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>() 获取该类下所有组件 需要注意在敌人脚本下方法要被调用需要public void 方法  调用时通过实体名.方法调用)  

### 关于敌人脚本管理
运用子父集管理(Enemy脚本为父 Enemy_XXX脚本为子 子类后的MonoBehaviour需要改成父类Enemy 需要注意父类方法需要public)  
子父集Start调用(父集脚本Enemy中定义的函数需要在前方加Protected来被子集调用 子集想调用父集方法也需要在方法前加Protected void Start下base.Start调用父集Start)  
子集绑定父集要调用自身组件(若子集有自身组件需要被添加时需要在父级改为Protected virtual void Start 子集改为protect override void Start)  
代码的修改(在碰撞判定时由于继承父类可以改为Enemy enemy = collision.gameObject.GetComponent<Enemy>() 此时触发的方法可以写入Enemy类)  

### 关于音效(未优化声音管理 目前全为手动添加组件)
添加声音(为想播放声音的物品添加Audio Source将想播放的声音拖入Audio Source--> Audio Clip 若为BGM出场播放勾选Play On Awake需要循环勾选Loop 实际收听组件在Camera下Audio Listener组件)  
敌人声音(Enemy类中protected AudioSource XXX在Start中获取AudioSource组件XXX.Play())  
关闭全局声音(GetComponent<AudioSource>().enable = false)  

### 关于场景
需要在脚本命名空间中引入(using UnityEngine.SceneManagement)  
人物死亡(在场景下方添加触发器 当人物碰撞时SceneManager.LoadScene(SceneManager.GetActiveScene().name)重置当前场景)  
死亡延迟重置(当触发器判定时候Invoke(“场景重置函数名”,延迟时间))  
场景传送(当按下某键时判断Input.GetKeyDown(KeyCode.XXX)后SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1)获取当前激活场景的下一个场景 需要将当前场景Build)  
Build场景(File--> Build Settings将场景拖入Scenes In Build中 场景后方的数字为buildIndex)  

### 关于2D光效
使背景暗下后添加光源(Tilemap Renderer--> Material--> Default-Diffuse)  
人物和物品材质球(详见关于材质球 将做好的材质球拖入人物和物品的材质球中)  
添加光源(Light--> Point Light光源为3D光源可以调整z值改变效果Light--> Range修改光照范围 Light--> Intensity修改亮度强度 若照出背景线可以修改地图Cell size或Edit--> Project Settings--> Quality--> Anti Aliasing--> Disabled)  

### 关于主菜单
新建场景后添加Panel(需要更改Image--> Source Image--> None把Panel四周圆角取消)  
背景添加(将图片拖入Image--> Source Image调整Image--> Color为白色)  
按钮(使用UI--> Button-TestMeshPro 作为PLAY和EXIT 按钮背景色根据Button下修改)  
字体渐变色(Text物体下勾选Color Gradient调色)  
PLAY和EXIT(PLAY修改场景切换同理 EXIT核心代码为Application.Quit())  
挂载脚本(挂在Button-TestMeshPro物体下后Button中On Click后添加物体选择物体对应函数 需要public函数)  
背景缓慢出现(录制动画Animation 修改不透明度)  
背景缓慢出现后显示文字(在动画结束后添加事件 先关闭UI后GameObject.Find(“绝对路径”.SetActive(true)))  

### 关于告示牌(已优化NewDialog.cs)
核心代码(transform.GetChild().GetChild().gameObject获取当前物体下的子物体GetChild括号中填写第几个物体的数字从0开始)  

#关于暂停
暂停按钮(使用Button-TestMeshPro)  
暂停界面(使用Panel)  
调整音量(使用UI--> Slider创建滑动条 滑动条颜色需要在子物体上修改 Create--> Aduio Mixer后Window--> AudioMixer打开设置窗口 把背景音乐的AudioSource--> Output挂上新创建的Master后调整Slider--> Min Value和Max Value的值(该游戏为Min -80 Max 0))  
返回游戏(使用Button-TestMeshPro)  
实现逻辑(通过事件实现窗口的SetActive)  
游戏时间停止和返回(Time.timeScale = 0f-1f   0为暂停1为恢复 0-1内可以使画面慢放)  
滑动条与AudioMixer关联(需要命名空间引用using UnityEngine.Audio后public AudioMixer xxx获取新建的AudioMixer 新建函数public void SetVolume(float value) 临时变量value获取AudioMixer的数值 xxx.SetFloat(“aa”,value) 需要注意先选中需要导出的值右键Expose”xx” to script变成可代码可编辑的模式后在AudioMixer窗口右上方Exposed Parameters中改成aa(与函数内部对应) 修改Silder中的事件调用该函数即可)  

### 关于手机移植(未实现 提供思路)
更改为手机模式(File--> BuildSettings 选择需要移植的平台)  
IOS平台(IOS设备下载Unity Remote 5 连接后Edit--> Project Settings--> Editor--> Device切换自己的设备)  
插件(使用Joystick Pack 下载导入后在Prefab中使用Variable Joystick 此预制体为UI类别 需要放入Canvas下 )  
代码连接(public Joystick xxx后代码中移动代码获得xxx.Horizontal即可 跳跃代码修改为xxx.Vertical > 0.5f 向上拖动一点摇杆即为跳跃 下蹲为xxx.Vertical < -0.5f 在Animator也需要相应修改)  

### 关于音效管理(未实现 提供思路)
准备工作(新建Sound Manager脚本 添加Audio Source)  
实现代码(public AudioSource xxx 和public AudioClip aaa,bbb,ccc,ddd 将对应的声音拖入)  
播放声音(以aaa为例 public void aaa下audioSource.clip = aaa后audioSource.Play)  
调用代码(思路为单例模式 public static SoundManager instance 后Private void Awake下instance = this在aaa声音播放方法下SoundManager.instance.aaa)  

  
