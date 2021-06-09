<template>
  <v-container>
    <v-sheet  elevation="5" class="pa-3">
      <router-link :to="{ name: 'ComponentPage', params: {id: component.id} }">
        <v-row >
          <v-col md="auto">
            <v-tooltip top nudge-top>
              <template v-slot:activator="{ on, attrs }">
                <div class="text-h5 text-truncate" v-bind="attrs"
                  v-on="on">Component</div>
              </template>
              <span>Component</span>
            </v-tooltip>
          
          </v-col>
        </v-row>
        <v-row>
          <v-col md="auto">
            <v-chip
              label
              dark
              small
              class="green"
            >Format</v-chip>
          </v-col>
          <v-col>
            <v-icon v-if="status" class="my-1" small>mdi-lock-outline</v-icon> 
          </v-col>
        </v-row>
        <v-row>
          <v-col>
            <div class="grey--text pa-1 text--darken-1">{{ component.author }}</div>
          </v-col>
          <v-col>
            <v-rating
              v-model="rating"
              background-color="purple lighten-3"
              color="green"
              small
            ></v-rating>
          </v-col>
        </v-row>
      </router-link>
    </v-sheet>
  </v-container>
</template>

<script>
export default {
  name: 'ComponentItem',
  props: ['component'],
  data: () => ({
    rating: 4.5
  }),
  computed: {
    
    status() {
      if(this.component.status == 'Private')
        return true
      else 
        return false
    }
  },
};
</script>
