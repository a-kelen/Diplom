<template>

 <v-row no-gutters>
      <v-col
        cols="14"
        sm="6"
        md="10"
        class="pa-6"
      >
        <v-row>
          <v-col>
            <v-text-field
            label="Search"
            placeholder="Input text"
            filled
            rounded
          ></v-text-field>
          </v-col>
        </v-row>
        <v-row v-if="selectedItem == 0">
          <v-col 
            v-for="n in 10" 
            :key="n"
            cols="4">
            <component-item />
          </v-col>
        </v-row>
        <v-row v-if="selectedItem == 1">
          <v-col 
            v-for="n in 10" 
            :key="n"
            cols="4">
            <library-item/>
          </v-col>
        </v-row>
        <v-row v-if="selectedItem == 2">
          <v-col 
            v-for="n in 10" 
            :key="n"
            cols="4">
            <author-item/>
          </v-col>
        </v-row>
      </v-col>
      <v-col
        cols="2"
        md="2"
      >
        <v-sheet elevation="6">
          <v-navigation-drawer permanent>
            <v-list-item active-class="highlighted" >
              <v-list-item-content>
                <v-list-item-title class="title">
                  Application
                </v-list-item-title>
                <v-list-item-subtitle>
                  subtext
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
            <v-divider></v-divider>
            <v-list
              dense
              nav
            >
              <v-list-item
                v-for="(item, i) in items"
                :key="i"
                :to="item.path" 
                active-class="highlighted"
                :class="item.path === $route.path ? 'highlighted' : ''"
              >
                <v-list-item-icon>
                  <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title>{{ item.title }}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-navigation-drawer>
          <router-view></router-view>
        </v-sheet>
      </v-col>
    </v-row>

</template>
  

<script>
import AuthorItem from '../components/items/AuthorItem.vue';
import ComponentItem from '../components/items/ComponentItem.vue';
import LibraryItem from '../components/items/LibraryItem.vue';


export default {
  name: 'Home',
  components: {
    ComponentItem,
    LibraryItem,
    AuthorItem
  },
  data: () => ({ 
      selectedItem: 1,
      items: [
        { title: 'Components', icon: 'mdi-view-compact', path: 'q1'},
        { title: 'Libraries', icon: 'mdi-library', path: 'q2' },
        { title: 'Users', icon: 'mdi-account', path: 'q3' },
      ],
    }),
    methods: {
      listHandler(em) {
        console.log(em);
      }
    }
};
</script>
