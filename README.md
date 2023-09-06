# PersonalProject2
2nd PersonalProject in NB camp


## 기본과제
### 1. 캐릭터 만들기

![image](https://github.com/Zif1519/PersonalProject2/assets/141081153/6f0cf7a7-e3f5-43e9-b8c8-fb6a323b68e3)


### 2. 캐릭터 이동구현하기

캐릭터 이동은 Movement_Grid와 Movement_Smooth 두가지 방식으로 구현하였고,
하나는 그리드 위에서 움직이는 방식으로 구현, 다른 하나는 그리드에 상관없이 움직이는 방식으로 구현
했습니다. 그리드 방식에서는 한번 움직이면 다음 움직임까지 일정 시간을 대기하는 방식으로 구현하였습니다.
다만, transform을 직접 변경하는 방식으로 만들어서 벽과의 충돌처리는 아직 미구현한 상태입니다.

### 3. 맵 생성하기

타일맵을 이용하여 맵을 생성하였고,
벽을 만들어주는 타일맵의 경우 TilemapCollider2D 와 CompositeCollider2D 를 이용하여 제작하였습니다.

![이미지 2](https://github.com/Zif1519/PersonalProject2/assets/141081153/43a2b609-7966-48d1-997b-68fd7a008935)

### 4. 카메라 이동시키기


        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class CameraMove : MonoBehaviour
        {
            public Camera _Camera;
            public GameObject _player;

            private void Start()
            {
                _Camera = GetComponent<Camera>();
                _player = GameObject.FindWithTag("Player");
            }

            private void LateUpdate()
            {
                _Camera.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
            }
        }


## 추가사항 (개인적으로 해본것들)
Draw.io를 통해서 프레임워크를 제작하는 과정에서 UML을 공부하게 되었고, 자연스럽게
객체지향설계에 대해서도 공부하게 되었습니다. 공부를 막 시작한 단계로써 정확히 이해를 마친
상태가 아니지만 UML 및 설계원칙에 맞추려는 노력으로 Component Diagram을 제작해 보았습니다.
또한 강의를 통해서 제작한 코드들을 객체지향원칙에 맞추어 Refactoring도 시도해 보았습니다.

### 1. Component Diagram 만들어보기

![ComponentDiagram2 drawio](https://github.com/Zif1519/PersonalProject2/assets/141081153/2e8ea7a6-10ad-444a-9937-71d4b88f9b51)

### 2. Refactoring 해보기

리펙토링 전의 코드입니다.
부모클래스로 TopDownCharacterController2D, 이를 상속받은 PlayerInputController 가 있었습니다.


        using System;
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class TopDownCharacterController : MonoBehaviour
        {
            public event Action<Vector2> OnMoveEvent;
            public event Action<Vector2> OnLookEvent;
            public event Action OnAttackEvent;

            private float _timeSinceLastAttack = float.MaxValue;

            protected bool IsAttacking { get; set; }

            protected virtual void Update()
            {
                HandleAttackDelay();
            }


            void Start()
            {
                
            }

            private void HandleAttackDelay()
            {
                if(_timeSinceLastAttack <= 0.2f)
                {
                    _timeSinceLastAttack += Time.deltaTime;
                }
                if (IsAttacking && _timeSinceLastAttack > 0.2f)
                {
                    _timeSinceLastAttack = 0f;
                    CallAttackEvent();
                }
            }

            public void CallMoveEvent(Vector2 direction)
            {
                OnMoveEvent?.Invoke(direction);
            }
            public void CallLookEvent(Vector2 direction) 
            { 
                OnLookEvent?.Invoke(direction);
            }
            public void CallAttackEvent()
            {
                OnAttackEvent?.Invoke();
            }
        }




        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.InputSystem;

        public class PlayerInputController : TopDownCharacterController
        {
            private Camera _camera;// Start is called before the first frame update
            private void Awake()
            {
                _camera = Camera.main;
            }

            public void OnMove(InputValue value)
            {
                //Debug.Log("OnMove" + value.ToString());
                Vector2 moveInput = value.Get<Vector2>().normalized;
                CallMoveEvent(moveInput);
            }
            public void OnLook(InputValue value)
            {
                //Debug.Log("OnLook" + value.ToString());
                Vector2 newAim = value.Get<Vector2>();
                Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
                Vector2 newAimDir = (worldPos - (Vector2)transform.position).normalized;

                if(newAimDir.magnitude >= 0.9f)
                {
                    CallLookEvent(newAimDir);
                }
            }

            public void OnFire(InputValue value)
            {
                IsAttacking = value.isPressed;
            }
        }


위의 두 클래스가 존재하였고, 게임 내에서는 자녀 클래스인 PlayerInputController를 사용중에 있었습니다.
저는 PlayerInput 과 연결된 InputActions에서 Action을 3가지 KeyboardInput, MouseMove, MouseClick 를 정의하고
이 PlayerInput을 받아주는 IController 라는 인터페이스와, 이를 상속받은 PlayerController로 이전의 두 클래스를 리펙토링 해보았습니다.

아래의 코드는 리펙토링 후의 코드입니다. 
인터페이스의 경우에는 OnKeyboardInput, OnMouseMove, OnMouseClick 라는 3개의 함수를 강제하여, PlayerInput에서 정의한 3가지
인풋에 대하여 모두 작동하도록 하는 인터페이스를 만들었습니다.
이 과정에서 부모클래스에 있었던 코드들은 모두 자녀코드로 옮기고 진행을 했습니다.


        using System;
        using UnityEngine;
        using UnityEngine.InputSystem;

        public interface IController
        {
                event Action<Vector2> OnKeyboardInputHandler;
                event Action<Vector2> OnMouseMoveHandler;
                event Action OnMouseClickHandler;

                void OnKeyboardInput(InputValue value);
                void OnMouseMove(InputValue value);
                void OnMouseClick(InputValue value);
        }


다음은 위의 인터페이스를 상속받은 자녀 클래스로 PlayerController의 코드입니다.



        using System;
        using UnityEngine;
        using UnityEngine.InputSystem;

        public class PlayerController : MonoBehaviour, IController
        {
            public event Action<Vector2> OnKeyboardInputHandler;
            public event Action<Vector2> OnMouseMoveHandler;
            public event Action OnMouseClickHandler;

            private Camera _camera;// Start is called before the first frame update
            private void Awake()
            {   _camera = Camera.main;   }

            public void OnKeyboardInput(InputValue value)
            {
                //Debug.Log("OnMove" + value.ToString());
                Vector2 moveInput = value.Get<Vector2>().normalized;
                OnKeyboardInputHandler?.Invoke(moveInput);
            }
            public void OnMouseMove(InputValue value)
            {
                //Debug.Log("OnLook" + value.ToString());
                Vector2 newAim = value.Get<Vector2>();
                Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
                Vector2 newAimDir = (worldPos - (Vector2)transform.position).normalized;
                OnMouseMoveHandler?.Invoke(newAimDir);
            }
            public void OnMouseClick(InputValue value)
            {
                OnMouseClickHandler?.Invoke();
            }
        }


인터페이스에서 강제된 3개의 함수는 인터페이스를 구체화한 PlayerController의 기능인 InputValue를
Player가 자주 활용하는 3가지 형태로 전환시키고, 그 값을 매개변수로 이벤트를 호출하는 방식으로 구현했습니다.
리펙토링 과정에서 불필요한 함수를 제거하고, TopDownShooting 이라는 코드의 실행을 위한 
재사용대기시간이 경과한지 여부를 판단하는 연산과정은 PlayerController에서 TopDownShooting 코드쪽으로 
이동시켰습니다. 이렇게 해서 PlayerController는 PlayerInput에서 들어온 InputValue을 받아서 3개의 이벤트를
호출한다는 기능만을 수행하도록 변경시켰습니다.
