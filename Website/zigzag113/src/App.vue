<template>
  <div id="app">
      <navbar></navbar>
      <router-view/>
      <vfooter></vfooter>
  </div>
</template>

<script>
import navbar from './components/navbar/Navbar.vue';
import vfooter from './components/Footer.vue';

export default {
  name: 'app',
  components: { navbar, vfooter }
}
</script>

<style>
    .no-margin {
        margin: 0px !important;
    }
    .no-padding {
        padding: 0px !important;
    }
    .thin {
        font-weight: 100 !important;
    }
    .spacer-1 {
        height: 10px !important;
    }
    .spacer-2 {
        height: 20px !important;
    }
    .spacer-4 {
        height: 40px !important;
    }
    .inline {
        display: inline-block !important;
    }
    @media only screen and (min-width: 1200px) {
        html {
            font-size: 18px;
        }
    }
</style>
