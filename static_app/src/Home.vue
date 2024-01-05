<template>
  <div class="container">
    <h1 class="text-center">What's the Fox Biz/Data Service WebApi?</h1>
  </div>
  <div class="container mt-3">
    <p>
      NeoDEEX 의 Fox Biz/Data Service 는 어플리케이션에 대한 비즈니스 로직과 데이터 액세스에 대한 서비스를 제공합니다.
      Fox Biz Service 는 사용자 정의 비즈니스 로직을 손쉽게 호출할 수 있는 환경을 제공하며 Fox Data Service 는
      myBatis 와 유사한 SQL Query를 사용하여 임의의 SQL 문장을 통해 데이터 베이스 접근을 용이하게 만들어 줍니다.
      뿐만아니라, Fox Biz/Data Service 는 서비스 호출에서 발생할 수 있는 다양한 문제를 해결하기 위해 로깅, 성능 측정,
      DB 액세스 정보 등을 포함하는 진단 정보를 기록하거나 클라이언트에게 반환 하는 기능을 포함합니다.
    </p>
    <p>
      Fox Biz/Data Service WebApi 는 Web Client 혹은 App Client 로부터 WebApi 호출을 수신하여 필요한 JSON 역직렬화를
      자동으로 수행하고 Fox Biz Service 혹은 Fox Data Service 를 호출합니다. 이를 통해 클라이언트는 사용자의 비즈니스
      로직이나 SQL 문장을 수행하고 그 결과를 수신할 수 있습니다.
    </p>
    <p>
      다음 그림은 Fox Biz/Data Service와 WebApi의 관계를 보여줍니다.
    </p>
    <div class="text-center">
      <img class="img-fluid rounded" src="./assets/about.png">
    </div>
  </div>
</template>